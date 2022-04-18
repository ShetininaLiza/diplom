using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisnessLogic.Models
{
    public enum Status
    {
        Поступила,
        Отклонена,
        Отправлена_рецензенту,
        Находится_на_рецензировании,
        Рецензирование_окончено,
        Устранить_замечания_редактора,
        Устранить_замечания_рецензента,
        Изменения_внесены,
        //Отправлена_на_дороботку_замечание_рецензента,
        //Отправлена_на_дороботку_замечание_редактора,
        Готова_к_публикации,
        Опубликована
    }
}
