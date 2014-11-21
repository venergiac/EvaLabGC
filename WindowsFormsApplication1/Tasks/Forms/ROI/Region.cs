using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace EVALabGC.ROI
{
    public class Region
    {

        public enum RegionType
        {
            Unknown,
            Permanent,
            Transient,
            Inverted
        }

        public enum RegionStatus
        {
            Active,
            Inactive
        }

        private Rectangle region = new Rectangle();
        private Action action = null;
        private RegionType type = RegionType.Unknown;
        private bool activated = false;

        public Region(int id, string type, int x, int y, int width, int height, string action, string[] actionParameters)
        {
            if (type.Equals("Permanent"))
            {
                this.type = RegionType.Permanent;
            }
            else if (type.Equals("Inverted"))
            {
                this.type = RegionType.Inverted;
            }
            else
            {
                this.type = RegionType.Transient;
            }

            this.region = new Rectangle(x, y, width, height);
            this.action = new Action(id,action, actionParameters, false);
        }

        public RegionStatus GetStatus(int x, int y)
        {
            if (activated) return RegionStatus.Active;
            if (type == RegionType.Inverted)
            {
                if (!(region.Contains(x, y)))
                {
                    return RegionStatus.Active;
                }
                return RegionStatus.Inactive;
            }
            else
            {
                if (region.Contains(x, y))
                {
                    activated = type == RegionType.Permanent;
                    return RegionStatus.Active;
                }
                return RegionStatus.Inactive;
            }
        }

        public Action Action{

            get
            {
                return action;
            }
        }
    }
}
