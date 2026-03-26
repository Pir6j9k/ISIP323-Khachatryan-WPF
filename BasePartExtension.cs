using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Khachatryan_WPF
{
    public partial class basepart
    {
        public string FullDescription
        {
            get
            {
                if (this.cpu != null)
                    return $"Ядер: {this.cpu.numberofcores} , Частота: {this.cpu.basecorefrequency} - {this.cpu.maxcorefrequency} ГГц, TDP: {this.cpu.thermalpower} W";

                if (this.gpu != null)
                    return $"Память: {this.gpu.videomemory} ГБ, Шина: {this.gpu.memorybus} бит, Рек. БП: {this.gpu.recommendpower}W";

                if (this.motherboard != null)
                    return $"Слотов ОЗУ: {this.motherboard.memoryslots}, SATA: {this.motherboard.sataports}, USB: {this.motherboard.usbports}";

                if (this.ram != null)
                    return $"Объем: {this.ram.capacity} ГБ ({this.ram.count} шт), Частота: {this.ram.ghz} МГц, Тайминги: {this.ram.timings}";

                if (this.powersupply != null)
                    return $"Мощность: {this.powersupply.power}W";

                if (this.processorcooler != null)
                    return $"Теплотрубок: {this.processorcooler.heatpipes}, Уровень шума: {this.processorcooler.noiselevel} дБ";

                if (this.storagedevice != null)
                    return $"Объем: {this.storagedevice.capacity} ГБ";

                if (this.@case != null)
                    return $"Слотов расширения: {this.@case.expansionslots}, Вентиляторов: {this.@case.fans}";

                return "Характеристики не указаны";
            }
        }
    }
}
