using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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

        private Text _lhsText;
        private Text _rhsText;
        private Text _neutralText;

        private void Start()
        {
            lhs.color = GerrymanderingUtil.GetColor(lhsFaction);
            rhs.color = GerrymanderingUtil.GetColor(rhsFaction);

            neutral.color = GerrymanderingUtil.GetColor(Faction.Neutral);

            _lhsTransform = lhs.rectTransform;
            _rhsTransform = rhs.rectTransform;
            _neutralTransform = neutral.rectTransform;

            _lhsTransform.localScale = Vector3.zero;
            _neutralTransform.localScale = Vector3.one;
            _rhsTransform.localScale = Vector3.zero;
            
            _lhsText = lhs.GetComponentInChildren<Text>();
            _rhsText = rhs.GetComponentInChildren<Text>();
            _neutralText = neutral.GetComponentInChildren<Text>();
            
            stateManager.CountyAmountUpdated.AddListener(CountyUpdated);
            
            // call it once for setup
            CountyUpdated();
        }

        private void CountyUpdated()
        {
            var votes = stateManager.VotesByFaction();
            
            AdjustSize(votes);
            AdjustText(votes);
            AdjustPosition();
        }
        
        private void AdjustSize([NotNull] Dictionary<Faction, int> votes)
        {
            if (votes == null) throw new ArgumentNullException(nameof(votes));
            
            var changePerVote = 1f / stateManager.MaxCountyAmount;                                                    // how much each county vote raises the scale
            var neutralVotes = votes[Faction.Neutral] + (stateManager.MaxCountyAmount - stateManager.CurrentCountyAmount);
            
            _lhsTransform.localScale = new Vector3(votes[lhsFaction] * changePerVote, 1f, 1f);
            _rhsTransform.localScale = new Vector3(votes[rhsFaction] * changePerVote, 1f, 1f);
            _neutralTransform.localScale = new Vector3(changePerVote * neutralVotes, 1f, 1f);
        }

        private void AdjustText([NotNull] IReadOnlyDictionary<Faction, int> votes)
        {
            _lhsText.text = "" + votes[lhsFaction];
            _rhsText.text = "" + votes[rhsFaction];
            _neutralText.text = "" + votes[Faction.Neutral];
        }

        private void AdjustPosition()
        {
            var lhsSize = _lhsTransform.sizeDelta.x * _lhsTransform.localScale.x;
            var neutralSize = _neutralTransform.sizeDelta.x * _neutralTransform.localScale.x;
            var rhsSize = _rhsTransform.sizeDelta.x * _rhsTransform.localScale.x;
            
            var nextLhsPosition = new Vector2(
                lhsSize * 0.5f,     /* Adjusting the Position based on the new size (always start at same position) */
                _lhsTransform.anchoredPosition.y);
            var nextNeutralPosition = new Vector2(
                lhsSize +           /* Adjusting the Position based on the new size of the other images */
                neutralSize * 0.5f,   /* Adjusting the Position based on the new size (always start at same position) */
                _neutralTransform.anchoredPosition.y);
            var nextRhsPosition = new Vector2(
                lhsSize + neutralSize +
                rhsSize * 0.5f,
                _rhsTransform.anchoredPosition.y);

            _lhsTransform.anchoredPosition = nextLhsPosition;
            _neutralTransform.anchoredPosition = nextNeutralPosition;
            _rhsTransform.anchoredPosition = nextRhsPosition;
        }
    }
}
