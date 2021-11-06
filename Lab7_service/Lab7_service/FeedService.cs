using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Text;

namespace Lab7_service
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "FeedService" в коде и файле конфигурации.
    public class FeedService : IFeedService
    {
        public SyndicationFeedFormatter GetStudentNotes(string studentId)
        {

        }
    }
}
