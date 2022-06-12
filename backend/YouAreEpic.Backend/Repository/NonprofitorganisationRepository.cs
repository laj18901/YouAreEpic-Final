using Bogus;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouAreEpic.Backend.Models;
using YouAreEpic.Backend.MongoDB;

namespace YouAreEpic.Backend.Repository.Implementations
{
    public interface INonprofitorganisationRepository : IMongoDBRepository<ObjectId, Nonprofitorganisation>, IRepository<ObjectId, Nonprofitorganisation> 
    {
        public Task<List<Nonprofitorganisation>> Seed(List<Category> categories);
    }

    public class NonprofitorganisationRepository : MongoDBRepository<ObjectId,Nonprofitorganisation>, INonprofitorganisationRepository
    {
        public NonprofitorganisationRepository(MongoSettings settings) : base(settings) { }

        public async Task<List<Nonprofitorganisation>> Seed(List<Category> categories)
        {
            /**var _npos = new Faker<Nonprofitorganisation>("de")
           .CustomInstantiator(f =>
           {
               var name = f.Lorem.Word();
               var imageLink = f.Image.PlaceImgUrl();

               return new Nonprofitorganisation
               {
                   CategoryIds = f.Make(2,
                       () => f.PickRandom(categories)
                   )
                   .Select(c => c.Id).ToList(),

                   Description = f.Lorem.Lines(3),
                   LogoLink = imageLink,
                   Name = f.Company.CompanyName(),
                   ShortDescription = f.Lorem.Lines(1),
                   WebsiteLink = f.Internet.Url()
               };
           })
           .Generate(4)
           .ToList();
            **/

            var kinder = categories.FirstOrDefault(c => c.Name == "Kinder");
            var umwelt = categories.FirstOrDefault(c => c.Name == "Umwelt");
            var tiere = categories.FirstOrDefault(c => c.Name == "Tiere");
            var forschung = categories.FirstOrDefault(c => c.Name == "Forschung");

            var _npos = new List<Nonprofitorganisation>
            {
                new Nonprofitorganisation
                {
                    Name = "WWF Österreich",
                    CategoryIds = new List<ObjectId> { tiere.Id, umwelt.Id},
                    Description = @"Wir wollen der weltweiten Naturzerstörung Einhalt gebieten und eine Zukunft gestalten, in der Mensch und Natur in Harmonie leben.



Daher ist es unser Ziel:



-die biologische Vielfalt der Erde zu bewahren,

-die naturverträgliche Nutzung erneuerbarer Ressourcen voranzutreiben,

-und Umweltverschmutzung und die Verschwendung von Naturgütern zu verhindern.



Der Schutz der Umwelt ist heute mehr denn je ein Thema. Es geht dabei nicht nur um exotische Tierarten, die vom Aussterben bedroht sind – wir wollen uns auch immer mit den sozialen Umständen befassen, die mit der weltweiten Naturzerstörung in Zusammenhang stehen.



In armen Ländern geht Armut oft einher mit Umweltzerstörung. In unseren reichen Industrienationen hingegen müssen wir uns auch mit unseren Konsumgewohnheiten auseinandersetzen, wollen wir unsere Umwelt langfristig erhalten.



Der WWF bietet Lösungen für Mensch und Natur – und zwar auf allen Ebenen



Bei Naturschutzprojekten mit fachlichem Know-How genauso wie bei politischen oder wirtschaftlichen Entscheidungsprozessen. Im Mittelpunkt steht dabei immer das Zusammenspiel von Mensch und Natur. Doch ohne die Unterstützung von möglichst vielen Menschen wird der Schutz der Natur auf Dauer nicht funktionieren.",
                    LogoLink = "https://www.wwf.at/wp-content/uploads/2019/11/WWF_Logo_frame.png",
                    ShortDescription = "Wir wollen die weltweite Zerstörung der Natur und Umwelt stoppen und eine Zukunft gestalten, in der Mensch und Natur in Harmonie leben.",
                    WebsiteLink = "https://www.wwf.at/",
                    TwitterUsername = "WWF"
                },
                new Nonprofitorganisation
                {
                    Name = "SOS Kinderdorf",
                    CategoryIds = new List<ObjectId> { kinder.Id},
                    Description = @"Kinderschutz steht immer an erster Stelle!


Das Wohl von Kindern ist für uns als SOS-Kinderdorf das oberste Ziel, dem wir alles andere unterordnen. Umso betroffener machen uns aktuelle Untersuchungen, die zeigen, dass der Schutz von Kindern in einzelnen internationalen Betreuungsangeboten von SOS-Kinderdorf nicht immer durchgängig gegeben war. Wir setzten sofort umfassende Maßnahmen zur konsequenten Aufarbeitung und Prävention.",
                    LogoLink = "https://www.sos-kinderdorf.at/getmedia/e03a8730-cfeb-491e-b90d-6f5ed670a174/logo-white.png",
                    ShortDescription = @"Was wir für die Kinder dieser Welt wollen
Jedem Kind ein liebevolles Zuhause!

Wir wollen an einer tatsächlichen gesellschaftlichen Veränderung mitarbeiten, die allen Kindern und Jugendlichen ein Aufwachsen in Würde und Wärme ermöglicht - geliebt, geachtet und behütet.",
                    WebsiteLink = "https://www.sos-kinderdorf.at/was-wir-tun",
                    TwitterUsername = "soskinderdorf"
                },
                new Nonprofitorganisation
                {
                    Name = "St. Anna Kinderkrebsforschung",
                    CategoryIds = new List<ObjectId> { forschung.Id, kinder.Id},
                    Description = "",
                    LogoLink = "https://kinderkrebsforschung.at/wp-content/uploads/sites/3/2021/11/logo.svg",
                    ShortDescription = "Jährlich sind etwa 300 Kinder und Jugendliche in Österreich von der Diagnose Krebs betroffen. Die Heilungsrate krebskranker Kinder und Jugendlicher zu verbessern – an dieser Zielsetzung arbeitet die St. Anna Kinderkrebsforschung seit 1988.",
                    WebsiteLink = "https://kinderkrebsforschung.at/",
                    TwitterUsername = "StAnna_CCRI"
                },
            };

            return await InsertManyAsync(_npos);
        }
    }
}
