using RAGE;
using RAGE.Ui;
using RAGE.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhantomCopsClient
{
    public class Account : Events.Script
    {
        private static HtmlWindow bLogin;
        public Account()
        {
            Events.Add("reloadCEF", SendCEFCommand);
            Events.Add("GetReadyToPlay", GetPlayerReadyToPlay);
            Events.Add("showError", ShowError);
            Events.Add("showSuccess", ShowSuccess);
            Events.Add("registerUser", RegisterUser);
            Events.Add("loginPlayer", LoginPlayer);

            Events.OnGuiReady += OnGuiReadyEvent;
        }

        private void LoginPlayer(object[] args)
        {
            Events.CallRemote("LoginPlayer", args[0].ToString(), args[1].ToString());
        }
        private void RegisterUser(object[] args)
        {
            Events.CallRemote("RegisterPlayer", args[0].ToString(), args[1].ToString(), args[2].ToString());
        }

        private void ShowError(object[] args)
        {
            bLogin.ExecuteJs($"mp.trigger(LoadError('{args[0].ToString()}'))");
        }

        private void ShowSuccess(object[] args)
        {
            bLogin.ExecuteJs($"mp.trigger(LoadSuccess('{args[0].ToString()}'))");
        }

        private void GetPlayerReadyToPlay(object[] args)
        {
            DestroyLoginBrowser();
            Ui.DisplayRadar(true);
            Ui.DisplayHud(true);
            Chat.Show(true);
            RAGE.Ui.Cursor.Visible = false;

        }

        private void SendCEFCommand(object[] args)
        {
            if(bLogin != null)
            {
                bLogin.Destroy();
            }
            RAGE.Chat.Output(args[0].ToString());
            bLogin = new HtmlWindow(args[0].ToString());
            RAGE.Ui.Cursor.Visible = true;
            bLogin.Active = true;
            bLogin.Reload(false);
        }

        private void DestroyLoginBrowser()
        {
            bLogin.Destroy();
            RAGE.Elements.Player.LocalPlayer.FreezePosition(false);
            Cam.RenderScriptCams(false, false, 0, true, false, 0);
        }

        private void OnGuiReadyEvent()
        {
            RAGE.Elements.Player.LocalPlayer.FreezePosition(true);

            var cam = Cam.CreateCameraWithParams(RAGE.Game.Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), 3400.0f, 5075.0f, 20.0f, 0.0f, 0.0f, 8.0f, 75.0f, true, 2);
            Cam.SetCamActive(cam, true);
            Cam.RenderScriptCams(true, false, 0, false, false, 0);

            Chat.Show(false);
            Chat.Activate(false);
            Ui.DisplayHud(false);
            Ui.DisplayRadar(false);
        }
    }
}
