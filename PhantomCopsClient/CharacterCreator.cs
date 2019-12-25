using RAGE;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RAGE.NUI;
using RAGE.Game;

namespace PhantomCopsClient
{
    public class CharacterCreator : Events.Script
    {
        public CharacterCreator()
        {
            Events.Add("ShowCharacterCreator", GenderMenu);
        }
        int[] fathers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 42, 43, 44 };
        int[] mothers = new int[] { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 45 };
        string[] fatherNames = new string[] { "Benjamin", "Daniel", "Joshua", "Noah", "Andrew", "Juan", "Alex", "Isaac", "Evan", "Ethan", "Vincent", "Angel", "Diego", "Adrian", "Gabriel", "Michael", "Santiago", "Kevin", "Louis", "Samuel", "Anthony", "Claude", "Niko", "John" };
        string[] motherNames = new string[] {"Hannah", "Aubrey", "Jasmine", "Gisele", "Amelia", "Isabella", "Zoe", "Ava", "Camila", "Violet", "Sophia", "Evelyn", "Nicole", "Ashley", "Gracie", "Brianna", "Natalie", "Olivia", "Elizabeth", "Charlotte", "Emma", "Misty"};
        private MenuPool _genderMenu;
        private MenuPool _parentsMenu;

        public void AddGenderMenu(UIMenu menu)
        {
            var Gender = new List<dynamic>
            {
                "Male",
                "Female"
            };

            var genderItem = new UIMenuListItem("Gender", Gender, 0);
            menu.AddItem(genderItem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == genderItem)
                {
                    Events.CallRemote("ChangePlayerGender", index);
                }
            };

            var confirm = new UIMenuItem("Confirm", "Confirm your gender!");
            confirm.SetLeftBadge(UIMenuItem.BadgeStyle.BronzeMedal);
            menu.AddItem(confirm);
            menu.OnIndexChange += (sender, index) =>
            {
                if (sender.MenuItems[index] == confirm)
                    ParentsMenuShow();
            };
        }

        public void ParentsMenuShow()
        {

        }

        int fatherid = 0;
        int motherid = 0;
        public void ParentsMenu(UIMenu menu)
        {
            var Fathers = new List<dynamic>
            {
                fathers,
                fatherNames,
            };

            var fatherItem = new UIMenuListItem("Father", Fathers, 0);
            menu.AddItem(fatherItem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == fatherItem)
                {
                    item.IndexToItem(index).ToString();
                    fatherid = index;
                }
            };

            var Mothers = new List<dynamic>
            {
                mothers,
                motherNames,
            };

            var motherItem = new UIMenuListItem("Father", Fathers, 0);
            menu.AddItem(motherItem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == motherItem)
                {
                    item.IndexToItem(index).ToString();
                    motherid = index;
                }
            };

            var parentsconf = new UIMenuItem("Confirm", "Confirm your parents!");
            parentsconf.SetLeftBadge(UIMenuItem.BadgeStyle.BronzeMedal);
            menu.AddItem(parentsconf);
            menu.OnIndexChange += (sender, index) =>
            {
                if (sender.MenuItems[index] == parentsconf)
                    ClothingMenuShow();
            };

        }

        public void ClothingMenuShow()
        {

        }



        public void UpdateParents()
        {
            
        }

        public void DrawMenu(List<RAGE.Events.TickNametagData> nametags)
        {
            _genderMenu.ProcessMenus();
        }

        public void GenderMenu(object[] args)
        {
            _genderMenu = new MenuPool();
            var genderMenu = new UIMenu("Gender Menu'", "Select your gender");

            genderMenu.FreezeAllInput = true;

            _genderMenu.Add(genderMenu);
            AddGenderMenu(genderMenu);
            _genderMenu.RefreshIndex();

            RAGE.Events.Tick += DrawMenu;

            genderMenu.Visible = true;
        }

        
    }
}
