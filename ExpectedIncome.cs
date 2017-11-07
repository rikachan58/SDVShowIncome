using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace ShowIncome
{
    public class ExpectedIncome : Mod
    {
        private ModConfig config;

        public override void Entry(IModHelper helper)
        {
            config = helper.ReadConfig<ModConfig>();
            ControlEvents.KeyPressed += this.ControlEvents_KeyPress;
        }

        private void ControlEvents_KeyPress(object sender, EventArgsKeyPressed e)
        {
             if (e.KeyPressed.ToString().Equals(config.ExpectedIncomeKey) && Context.IsWorldReady)
             {
                Game1.showGlobalMessage(CalculateIncome());    
             }
        }

        private String CalculateIncome()
        {
            //Get items in the ShippingBin, then calculate the income
            List<Item> currentShippedItems = Game1.getFarm().shippingBin;
            int income = 0;
            if (currentShippedItems.Count != 0)
            {
                foreach (Item item in currentShippedItems)
                {
                    if (item is StardewValley.Object obj)
                    {
                        income += obj.sellToStorePrice() * obj.getStack();
                    }
                }
            }
            String expectedIncome = "Expected Income: " + income + "g.";
            //Return the expected income 
            return expectedIncome;
        }
    }

    public class ModConfig
    {
        public String ExpectedIncomeKey { get; set; }

        public ModConfig()
        {
            this.ExpectedIncomeKey = "I";
        }
    }

}