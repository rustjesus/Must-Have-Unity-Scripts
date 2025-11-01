using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace TMG_Inventory
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        private float gold;
        private float silver;
        private float copper;

        //totals
        [HideInInspector] public int g;
        [HideInInspector] public int s;
        [HideInInspector] public int c;


        //reward totals
        [HideInInspector] public int rGold;
        [HideInInspector] public int rSilver;
        [HideInInspector] public int rCopper;
        public Color copperColor = Color.white;
        public Color silverColor = Color.grey;
        public Color goldColor = Color.yellow;
        [HideInInspector] public float totalCoinsRightNow;
        void Start()
        {
            totalCoinsRightNow = PlayerPrefs.GetFloat("TotalCoin");
            CalculateCurrentMoney();
        }

        public void GiveRewardMoney(int rewardMoney)
        {
            totalCoinsRightNow = totalCoinsRightNow + rewardMoney;

            CalculateCurrentMoney();
        }
        public void TakeMoney(int rewardMoney)
        {
            totalCoinsRightNow = totalCoinsRightNow - rewardMoney;

            CalculateCurrentMoney();
        }
        public void SaveMoneyData()
        {
            PlayerPrefs.SetFloat("TotalCoin", totalCoinsRightNow);
        }
        public string RewardCoins(int reward)
        {
            // Calculate the amount of each coin type
            (int gold, int silver, int copper) = CalculateThisMoney(reward);

            // Change text color for each coin string
            string goldTxt = ColorString(gold + " Gold", goldColor);
            string silverTxt = ColorString(silver + " Silver", silverColor);
            string copperTxt = ColorString(copper + " Copper", copperColor);

            // Reward coins to the player
            GiveRewardMoney(reward);
            CalculateRewardMoney(reward);

            // Build the debug string
            string debugMessage = goldTxt + ", " + silverTxt + ", " + copperTxt;

            // Log the message and return it
            Debug.Log(debugMessage);
            return debugMessage;
        }
        private void CalculateCurrentMoney()
        {
            float coin = totalCoinsRightNow;
            if (coin != 0)
            {
                gold = coin / 10000; g = (int)gold;
                silver = (coin - (g * 10000)) / 100; s = (int)silver;
                copper = coin - (g * 10000) - (s * 100); c = (int)copper;
            }
            else
            {
                g = 0;
                s = 0;
                c = 0;
            }
            string goldTxt = ColorString(g + " Gold", goldColor);
            string silverTxt = ColorString(s + " Silver", silverColor);
            string copperTxt = ColorString(c + " Copper", copperColor);

            moneyText.text = "Money: " + goldTxt + ", " + silverTxt + ", " + copperTxt;
        }
        public static string ColorString(string text, Color color)
        {
            return "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + text + "</color>";
        }
        //for things like popup text
        public void CalculateRewardMoney(int reward)
        {
            float gold;
            float silver;
            float copper;

            if (reward != 0)
            {
                gold = reward / 10000; rGold = (int)gold;
                silver = (reward - (rGold * 10000)) / 100; rSilver = (int)silver;
                copper = reward - (rGold * 10000) - (rSilver * 100); rCopper = (int)copper;
            }
            else
            {
                rGold = 0;
                rSilver = 0;
                rCopper = 0;
            }
        }

        public string ReturnMoneyAsColoredGSC_String(int money)
        {
            var (g, s, c) = CalculateThisMoney(money);

            string coloredGold = g > 0 ? ColorString($"{g}g", goldColor) : "";
            string coloredSilver = s > 0 ? ColorString($"{s}s", silverColor) : "";
            string coloredCopper = c > 0 ? ColorString($"{c}c", copperColor) : "";

            string separator = ", ";
            string text = string.Join(separator, new[] { coloredGold, coloredSilver, coloredCopper }.Where(x => !string.IsNullOrEmpty(x)));

            return text;
        }
        public static (int gold, int silver, int copper) CalculateThisMoney(float coin)
        {
            int g;
            int s;
            int c;
            if (coin != 0)
            {
                float gold = coin / 10000;
                g = (int)gold;
                float silver = (coin - (g * 10000)) / 100;
                s = (int)silver;
                float copper = coin - (g * 10000) - (s * 100);
                c = (int)copper;
            }
            else
            {
                g = 0;
                s = 0;
                c = 0;
            }
            return (g, s, c);
        }

    }
}

