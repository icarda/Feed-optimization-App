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
        public const decimal ME_maintenance_EWES = 93;
        public const decimal ME_maintenance_EWES_AND_LAMBS = 93;
        public const decimal ME_maintenance_WEANED_LAMBS = 110;
        public const decimal ME_maintenance_RAMS = 107;
        public const decimal ME_m_GRAZING_NONE = 0;
        public const decimal ME_m_GRAZING_CLOSE_BY = 0.25m;
        public const decimal ME_m_GRAZING_OPEN_RANGE = 0.5m;
        public const decimal ME_m_GRAZING_ROUGH_MOUNTAIN_TERRAIN = 0.75m;
        public const decimal ME_gain_EWES = 0.83m;
        public const decimal ME_gain_EWES_AND_LAMBS = 0.83m;
        public const decimal ME_gain_WEANED_LAMBS = 0.4m;
        public const decimal ME_gain_RAMS = 0.83m;
        public const decimal ME_gestation_YES = 1.5m;
        public const decimal ME_gestation_NO = 0;
        public const decimal ME_lactation = 1250;
        public const decimal DCP_Maintenance_EWES = 18.5m;
        public const decimal DCP_Maintenance_EWES_AND_LAMBS = 18.5m;
        public const decimal DCP_Maintenance_WEANED_LAMBS = 67.0m;
        public const decimal DCP_Maintenance_RAMS = 45.3m;
        public const decimal DCP_Lactation_LOW = 27;
        public const decimal DCP_Lactation_HIGH = 30;
        public const decimal CP_Lactation_LOW = 33.9m;
        public const decimal CP_Lactation_HIGH = 37.3m;
        public const decimal CP_gain_EWES = 24.5m;
        public const decimal CP_gain_EWES_AND_LAMBS = 24.5m;
        public const decimal CP_gain_WEANED_LAMBS = 78.5m;
        public const decimal CP_gain_RAMS = 54.3m;
        public const decimal DQE_EWES_LOW = 35;
        public const decimal DQE_EWES_MEDIUM = 64.4m;
        public const decimal DQE_EWES_HIGH = 74;
        public const decimal DQE_EWES_AND_LAMBS_LOW = 35;
        public const decimal DQE_EWES_AND_LAMBS_MEDIUM = 64.4m;
        public const decimal DQE_EWES_AND_LAMBS_HIGH = 74;
        public const decimal DQE_WEANED_LAMBS_LOW = 35;
        public const decimal DQE_WEANED_LAMBS_MEDIUM = 64.4m;
        public const decimal DQE_WEANED_LAMBS_HIGH = 74;
        public const decimal DQE_RAMS_LOW = 120;
        public const decimal DQE_RAMS_MEDIUM = 120;
        public const decimal DQE_RAMS_HIGH = 120;
        public const decimal DMI_gestation_YES = 0.4m;
        public const decimal DMI_gestation_NO = 0;
        public const decimal DMI_lactation = 1.14m;
        public const decimal DMI_lactation_HIGH = 1.70m;
    }
}