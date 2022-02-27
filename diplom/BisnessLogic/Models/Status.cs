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
        Отправлена_на_дороботку,
        Готоав_к_публикации,
        Публикуется_в_слудующем_номере
    }
}
