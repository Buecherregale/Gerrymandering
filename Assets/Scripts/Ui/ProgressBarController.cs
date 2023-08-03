using System.Linq;
using Manager;
using Model;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Ui
{
    public class ProgressBarController : MonoBehaviour
    {
        [SerializeField] private StateManager stateManager;

        [SerializeField] private Image lhs;
        [SerializeField] private Image rhs;
        [SerializeField] private Image neutral;

        [SerializeField] private Faction lhsFaction;
        [SerializeField] private Faction rhsFaction;

        private RectTransform _lhsTransform;
        private RectTransform _rhsTransform;
        private RectTransform _neutralTransform;

        private void Start()
        {
            lhs.color = GerrymanderingUtil.GetColor(lhsFaction);
            rhs.color = GerrymanderingUtil.GetColor(rhsFaction);

            neutral.color = GerrymanderingUtil.GetColor(Faction.Neutral);

            _lhsTransform = lhs.rectTransform;
            _rhsTransform = rhs.rectTransform;
            _neutralTransform = rhs.rectTransform;
            
            stateManager.countyAmountUpdated.AddListener(CountyUpdated);
        }

        /// <summary>
        /// aus irgendeinem Grund werden die Scales beide updated außerdem ist das positioning fucked
        /// </summary>
        private void CountyUpdated()
        {
            var votes = stateManager.VotesByFaction();
            
            stateManager.VotesByFaction().Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Debug.Log);

            _lhsTransform.localScale = new Vector3((float) votes[lhsFaction] / stateManager.MaxCountyAmount, 1f, 0f);
            _rhsTransform.localScale = new Vector3((float) votes[rhsFaction] / stateManager.MaxCountyAmount, 1f, 0f);
            Debug.Log(_rhsTransform.localScale.x + " DA: " + votes[rhsFaction]);
            _neutralTransform.localScale =
                new Vector3((float) stateManager.CurrentCountyAmount / stateManager.MaxCountyAmount + 
                    (float) votes[Faction.Neutral] / stateManager.MaxCountyAmount, 1f, 0f);

            var lhsPosition = _lhsTransform.localPosition;
            lhsPosition.x = 0;
            _lhsTransform.localPosition = lhsPosition;
            
            var neutralPosition = _neutralTransform.position;
            neutralPosition.x += _lhsTransform.rect.width;
            _neutralTransform.localPosition = neutralPosition;
            
            var rhsPosition = _rhsTransform.position;
            rhsPosition.x += _neutralTransform.rect.width;
            _rhsTransform.localPosition = rhsPosition;
            
            Debug.Log("After: " + _rhsTransform.localScale.x);
        }
    }
}
