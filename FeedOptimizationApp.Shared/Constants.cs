using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FeedOptimizationApp.Shared
{
    public class Constants
    {
        public const double ME_maintenance_EWES = 93;
        public const double ME_maintenance_EWES_AND_LAMBS = 93;
        public const double ME_maintenance_WEANED_LAMBS = 110;
        public const double ME_maintenance_RAMS = 107;
        public const double ME_m_GRAZING_NONE = 0;
        public const double ME_m_GRAZING_CLOSE_BY = 0.25;
        public const double ME_m_GRAZING_OPEN_RANGE = 0.5;
        public const double ME_m_GRAZING_ROUGH_MOUNTAIN_TERRAIN = 0.75;
        public const double ME_gain_EWES = 0.83;
        public const double ME_gain_EWES_AND_LAMBS = 0.83;
        public const double ME_gain_WEANED_LAMBS = 0.4;
        public const double ME_gain_RAMS = 0.83;
        public const double ME_gestation_YES = 1.5;
        public const double ME_gestation_NO = 0;
        public const double ME_lactation = 1250;
        public const double DCP_Maintenance_EWES = 18.5;
        public const double DCP_Maintenance_EWES_AND_LAMBS = 18.5;
        public const double DCP_Maintenance_WEANED_LAMBS = 67.0;
        public const double DCP_Maintenance_RAMS = 45.3;
        public const double DCP_Lactation_LOW = 27;
        public const double DCP_Lactation_HIGH = 30;
        public const double CP_Lactation_LOW = 33.9;
        public const double CP_Lactation_HIGH = 37.3;
        public const double CP_gain_EWES = 24.5;
        public const double CP_gain_EWES_AND_LAMBS = 24.5;
        public const double CP_gain_WEANED_LAMBS = 78.5;
        public const double CP_gain_RAMS = 54.3;
        public const double DQE_EWES_LOW = 35;
        public const double DQE_EWES_MEDIUM = 64.4;
        public const double DQE_EWES_HIGH = 74;
        public const double DQE_EWES_AND_LAMBS_LOW = 35;
        public const double DQE_EWES_AND_LAMBS_MEDIUM = 64.4;
        public const double DQE_EWES_AND_LAMBS_HIGH = 74;
        public const double DQE_WEANED_LAMBS_LOW = 35;
        public const double DQE_WEANED_LAMBS_MEDIUM = 64.4;
        public const double DQE_WEANED_LAMBS_HIGH = 74;
        public const double DQE_RAMS_LOW = 120;
        public const double DQE_RAMS_MEDIUM = 120;
        public const double DQE_RAMS_HIGH = 120;
        public const double DMI_gestation_YES = 0.4;
        public const double DMI_gestation_NO = 0;
        public const double DMI_lactation = 1.14;
        public const double DMI_lactation_HIGH = 1.70;
    }
}