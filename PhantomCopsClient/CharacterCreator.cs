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
        int gender = 0;
        float mix = 9.0f;
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
                    gender = index;
                }
            };

            var confirm = new UIMenuItem("Confirm", "Confirm your gender!");
            confirm.SetLeftBadge(UIMenuItem.BadgeStyle.BronzeMedal);
            menu.AddItem(confirm);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (sender.MenuItems[index] == confirm)
                    ParentsMenuShow();
            };
        }

        public void DrawGenderMenu(List<RAGE.Events.TickNametagData> nametags)
        {
            _genderMenu.ProcessMenus();
        }

        public void GenderMenu(object[] args)
        {
            SetupCamera();
            RAGE.Elements.Player.LocalPlayer.FreezePosition(true);

            var genderMenu = new UIMenu("Gender Menu'", "Select your gender");

            _genderMenu = new MenuPool();
            genderMenu.FreezeAllInput = true;

            _genderMenu.Add(genderMenu);
            AddGenderMenu(genderMenu);
            _genderMenu.RefreshIndex();

            RAGE.Events.Tick += DrawGenderMenu;

            genderMenu.Visible = true;
        }

        public void ParentsMenuShow()
        {
            _genderMenu.CloseAllMenus();
            _parentsMenu = new MenuPool();
            var parentsMenu = new UIMenu("Parents Menu'", "Select your parents");

            parentsMenu.FreezeAllInput = true;

            _parentsMenu.Add(parentsMenu);
            AddParentsMenu(parentsMenu);
            _parentsMenu.RefreshIndex();

            RAGE.Events.Tick += DrawParentsMenu;

            parentsMenu.Visible = true;


        }

        public void DrawParentsMenu(List<RAGE.Events.TickNametagData> nametags)
        {
            _parentsMenu.ProcessMenus();
        }

        public void AddParentsMenu(UIMenu menu)
        {

            int fatherid = 0;
            int motherid = 0;
            int similarity = 0;

            var Fathers = new List<object>(fatherNames);

            var fatherItem = new UIMenuListItem("Father", Fathers, 0);
            menu.AddItem(fatherItem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == fatherItem)
                {
                    item.IndexToItem(index).ToString();
                    UpdateParents(fathers[fatherid], mothers[motherid], similarity);
                    fatherid = index;
                }
            };

            var Mothers = new List<object>(motherNames);

            var motherItem = new UIMenuListItem("Mothers", Mothers, 0);
            menu.AddItem(motherItem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == motherItem)
                {
                    item.IndexToItem(index).ToString();
                    UpdateParents(fathers[fatherid], mothers[motherid], similarity);
                    motherid = index;
                }
            };

            var SimIndex = new List<object>();

            for (int i = 0; i < 10; i++)
            {
                SimIndex.Add(i);
            }


            var simItem = new UIMenuListItem("Similarity to Mother", SimIndex, 0);
            menu.AddItem(simItem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == simItem)
                {
                    UpdateParents(fathers[fatherid], mothers[motherid], similarity);
                    similarity = index;
                }
            };

            var parentsconf = new UIMenuItem("Confirm", "Confirm Your Character!");
            parentsconf.SetLeftBadge(UIMenuItem.BadgeStyle.BronzeMedal);
            menu.AddItem(parentsconf);
            menu.OnItemSelect += (sender, item, index) =>
            {
                //ClothingMenuShow();
                RAGE.Events.CallRemote("ConfirmParents", fathers[fatherid], mothers[motherid], mix);
            };

        }

        public void AddClothingMenu(UIMenu menu)
        {
            string Gender;
            if (gender == 0) Gender = "male";
            else Gender = "female";


        }

        public void SetupCamera()
        {
            var cam = Cam.CreateCameraWithParams(RAGE.Game.Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), 402.8664f, -997.5515f, -98.5f, 0.0f, 0.0f, 8.0f, 75.0f, true, 2);
            Cam.SetCamActive(cam, true);
            Cam.RenderScriptCams(true, false, 0, false, false, 0);
        }

        public void UpdateParents(int fatherid, int motherid, int sim)
        {
            switch(sim)
            {
                case 0:
                    {
                        mix = 0.0f;
                        break;
                    }
                case 1:
                    {
                        mix = 0.1f;
                        break;
                    }
                case 2:
                    {
                        mix = 0.2f;
                        break;
                    }
                case 3:
                    {
                        mix = 0.3f;
                        break;
                    }
                case 4:
                    {
                        mix = 0.4f;
                        break;
                    }

                case 5:
                    {
                        mix = 0.5f;
                        break;
                    }
                case 6:
                    {
                        mix = 0.6f;
                        break;
                    }
                case 7:
                    {
                        mix = 0.7f;
                        break;
                    }
                case 8:
                    {
                        mix = 0.8f;
                        break;
                    }
                case 9:
                    {
                        mix = 0.9f;
                        break;
                    }
                case 10:
                    {
                        mix = 1.0f;
                        break;
                    }
            }
            RAGE.Chat.Output("MIX: " + mix);
            Events.CallRemote("UpdatePlayerParents", fatherid, motherid, mix);
        }

        
    }
}
