using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatsTestSystem.HardwareSerialNumberWirter.Models
{
    class ComboboxDataUsedInInitialSNinfoWindow
    {
        public List<ModelSelect> modelSelects = new List<ModelSelect>()
            {
                new ModelSelect(){modelSelect = "单路 0x02"},
                new ModelSelect(){modelSelect = "双路6.125m道间距版本 0x03"},
                new ModelSelect(){modelSelect = "双路12.5m道间距版本 0x04"},
                new ModelSelect(){modelSelect = "双路3.125m道间距版本 0x05"}
            };

        public List<PCBASelected> pCBASelecteds = new List<PCBASelected>()
            {
                new PCBASelected(){pcbaSelected="元森快捷制版 / 无锡鸿睿焊接 0x00"},
                new PCBASelected(){pcbaSelected="崇达制版/凌华焊接 0x01"}
            };

        public List<Year> years = new List<Year>()
            {
                new Year(){year=(DateTime.Now.Year-1).ToString()},
                new Year(){year=(DateTime.Now.Year).ToString()},
                new Year(){year=(DateTime.Now.Year+1).ToString()},
            };

        public List<WeekOfYear> weekOfYears = new List<WeekOfYear>()
            {
                new WeekOfYear(){weekofyear = (DateTime.Now.DayOfYear/7).ToString()},
                new WeekOfYear(){weekofyear = (DateTime.Now.DayOfYear/7 + 1).ToString()},
                new WeekOfYear(){weekofyear = ((DateTime.Now.DayOfYear/7)+2).ToString()},
            };
    }
    class ModelSelect
    {
        public string modelSelect { get; set; }
    }

    class PCBASelected
    {
        public string pcbaSelected { get; set; }
    }

    class Year
    {
        public string year { get; set; }
    }

    class WeekOfYear
    {
        public string weekofyear { get; set; }
    }
}
