using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FortyFiftySixDeegrees.Models;

namespace FortyFiftySixDeegrees.Helper
{
    [Serializable]
    public class PortalSession
    {
        public List<PartialOrder> partialOrders {set; get;}

        public PortalSession()
        {
            partialOrders = new List<PartialOrder>();
        }

        public void SaveUserInSessionState(HttpContext context)
        {
            if (context != null)
            {
                context.Session["PortalSession"] = this;
            }
        }

        /// <summary>
        /// This method save the PortalSession (this) object in the context
        /// </summary>
        public void SaveUserInSessionState()
        {
            SaveUserInSessionState(HttpContext.Current);
        }

        /// <summary>
        /// Returns the PortalSession object contained into the context Session.
        /// If the session do not exist, a new PortalSession object will be created.
        /// If the session do not contains a PortalSession object a new one obect will be created.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>a PortalSession object</returns>
        public static PortalSession GetSession(HttpContext context)
        {
            PortalSession session = null;

            if ((context != null) && (context.Session != null) && (context.Session["PortalSession"] != null))
            {
                session = (PortalSession)context.Session["PortalSession"];
            }

            if (session == null)
            {
                session = new PortalSession();
            }

            return session;
        }

        /// <summary>
        /// Returns the PortalSession object contained into the context Session.
        /// </summary>
        /// <returns>PortalSession object</returns>
        public static PortalSession GetSession()
        {
            return GetSession(HttpContext.Current);
        }

    }
}