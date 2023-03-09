using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace NaiveAPI.ItemSystem
{
    [System.Serializable]
    public class LootPool
    {
        public List<LootBag> LootBags = new List<LootBag>();
        [SerializeField] private EmptyBehavior behavior = EmptyBehavior.BALANCE;
        public EmptyBehavior Behavior
        {
            get
            {
                return behavior;
            }
        }

        private float percentLeft = 0;
        private float[] originalProbabilities;
        private float[] probabilities;

        public enum EmptyBehavior
        {
            NONE, BALANCE
        }

        public void Add(LootBag lootBag, EmptyBehavior emptyBehavior = EmptyBehavior.BALANCE)
        {
            behavior = emptyBehavior;
            LootBags.Add(new LootBag(lootBag.Loots, lootBag.IsLimited, lootBag.Percent));
            SetActualProbability();
        }

        public void AddRange(LootBag[] lootBags, EmptyBehavior emptyBehavior = EmptyBehavior.BALANCE)
        {
            behavior = emptyBehavior;
            for (int i = 0;i<lootBags.Length;i++)
            {
                LootBags.Add(new LootBag(lootBags[i].Loots, lootBags[i].IsLimited, lootBags[i].Percent));
            }
            SetActualProbability();
        }

        public SOItemBase Get()
        {
            if (LootBags.Count == 0) return null;
            SOItemBase item;
            float randomNum = Random.Range(0f, 100f);
            float[] relatedPercent = GetRelatedProbability();
            int index = 0;

            for (int i = 0;i < relatedPercent.Length; i++)
            {
                if (relatedPercent[i] > randomNum) break;
                index = i;
            }

            if (index == LootBags.Count) return null;

            item = LootBags[index].Get();

            if (LootBags[index].Loots.Count == 0)
            {
                if (behavior == EmptyBehavior.BALANCE)
                {
                    SetNewProbabilities(index);
                }
                else if (behavior == EmptyBehavior.NONE)
                {
                    
                }
                //LootBags.RemoveAt(index);
                //PrintProbabilities(GetRelatedProbability());
                Debug.Log(ToString());
            }

            return item;
        }

        private void SetNewProbabilities(int index)
        {
            percentLeft += originalProbabilities[index];
            float proportion = 100 / (100 - percentLeft);
            originalProbabilities[index] = 0f;
            for (int i = 0;i < probabilities.Length; i++)
            {
                probabilities[i] = originalProbabilities[i] * proportion;
            }
        }

        private void SetActualProbability()
        {
            if (LootBags.Count == 0) return;
            originalProbabilities = new float[LootBags.Count + 1];
            probabilities = new float[LootBags.Count + 1];
            float remainedPercent = 1f;
            for (int i = 0;i < LootBags.Count; i++)
            {
                originalProbabilities[i] = LootBags[i].Percent * remainedPercent;
                probabilities[i] = LootBags[i].Percent * remainedPercent;

                remainedPercent -= (originalProbabilities[i] / 100f);
            }

            originalProbabilities[LootBags.Count] = remainedPercent * 100;
            probabilities[LootBags.Count] = remainedPercent * 100;
        }

        public float[] GetActualProbability()
        {
            return probabilities;
        }

        public float[] GetRelatedProbability()
        {
            if (probabilities == null) return null;
            float totalPercent = 0f;
            float[] output = new float[probabilities.Length];

            for (int i = 0; i < probabilities.Length - 1; i++)
            {
                output[i] = totalPercent;
                totalPercent += probabilities[i];
            }
            output[probabilities.Length - 1] = totalPercent;
            return output;
        }

        public void PrintProbabilities(float[] probability)
        {

            if (probability == null) return;
            StringBuilder str = new StringBuilder("Probibilities : [ " + string.Format("{0:0.00}", probability[0]) + "%");
            for (int i = 1; i < probability.Length; i++)
            {
                str.Append(", " + string.Format("{0:0.00}", probability[i]) + "%");
            }
            Debug.Log(str + " ]");
        }

        public override string ToString()
        {
            if (probabilities == null) return "NONE";
            StringBuilder str = new StringBuilder("Probibilities : [ " + string.Format("{0:0.00}", probabilities[0]) + "%");
            for (int i = 1; i < probabilities.Length; i++)
            {
                str.Append(", " + string.Format("{0:0.00}", probabilities[i]) + "%");
            }
            return str + " ]";
        }
    }
}
