using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisnessLogic.Models
{
    public enum Status
    {
        Принята,
        Отклонена,
        Отправлена_рецензенту,
        Находится_на_рецензировании,
        Рецензирование_окончено,
        Отправлена_на_дороботку,
        Готова_к_публикации,
        Опубликована,
        Публикуется_в_слудующем_номере
    }
}
