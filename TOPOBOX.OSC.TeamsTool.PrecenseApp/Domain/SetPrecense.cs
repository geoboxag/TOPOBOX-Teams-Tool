using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOPOBOX.OSC.TeamsTool.Common.GraphHelper;

namespace TOPOBOX.OSC.TeamsTool.PrecenseApp.Domain
{
    internal class SetPrecense
    {
        private GraphConnectorHelper connectorHelper;
        private string sessionId;
        private string expirationDuration;

        internal SetPrecense(BatchRuntimeSettings runtimeSettings)
        {
            connectorHelper = runtimeSettings.GraphConnectorHelper;
            sessionId = runtimeSettings.SessionID;
            expirationDuration = runtimeSettings.ExpirationDuration;
        }

        internal bool SetAvailable(string userid)
        {
            return SetPresence(userid, "Available", "Available");
        }

        internal bool SetBusy(string userid, bool inAConferenceCall)
        {
            string availability = "Busy";
            string activity = "InACall";
            if (inAConferenceCall) activity = "InAConferenceCall";

            return SetPresence(userid, availability, activity);
        }

        internal bool SetAway(string userid)
        {
            return SetPresence(userid, "Away", "Away");
        }

        internal bool Reset(string userid)
        {
            return ResetPresence(userid);
        }

        private bool SetPresence(string userid, string availability, string activity)
        {
            Duration duration = new Duration(expirationDuration);
            var graphRequest = connectorHelper.GraphServiceClient.Users[userid].Presence.SetPresence(sessionId, availability, activity, duration).Request();

            var task = new Task<bool>(gR =>
            {
                var request = gR as PresenceSetPresenceRequest;
                
                var answer = request.PostAsync();

                return answer.IsCompleted && !answer.IsFaulted;
            }, graphRequest);

            task.Start();
            task.Wait();
            return task.Result;
        }


        private bool ResetPresence(string userid)
        {
            var graphRequest = connectorHelper.GraphServiceClient.Users[userid].Presence.ClearPresence(sessionId).Request();

            var task = new Task<bool>(gR =>
            {
                var request = gR as PresenceSetPresenceRequest;

                var answer = request.PostAsync();

                return answer.IsCompleted && !answer.IsFaulted;
            }, graphRequest);

            task.Start();
            task.Wait();
            return task.Result;
        }
    }
}