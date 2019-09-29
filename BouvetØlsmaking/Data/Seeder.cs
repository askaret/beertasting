using BouvetØlsmaking.Models;
using System.Collections.Generic;
using System.Linq;

namespace BouvetØlsmaking.Data
{
    public static class Seeder
    {
        public static void Seed(TastingContext context)
        {
            context.Database.EnsureCreated();

            if (context.Beer.Any())
            {
                return;
            }

            var beerClasses = new List<string>()
            {
                "Regular",
                "Homebrew",
                "High ABV"
            };
            var classes = new List<BeerClass>();
            for (var i = 0; i < beerClasses.Count; i++)
            {
                classes.Add(new BeerClass()
                {
                    Name = beerClasses[i]
                });
            }
            context.Beerclass.AddRange(classes);
            context.SaveChanges();

            var styleNames = new List<string>()
            {
                "Alternative Grain Beer",
"Alternative Sugar Beer",
"American Amber Ale",
"American Barleywine",
"American Brown Ale",
"American IPA",
"American Lager",
"Altbier",
"American Light Lager",
"American Pale Ale",
"American Porter",
"American Stout",
"American Strong Ale",
"American Wheat Beer",
"American Wheat or Rye Beer",
"Apple Wine",
"Australian Sparkling Ale",
"Autumn Seasonal Beer",
"Baltic Porter",
"Belgian Blond Ale",
"Belgian Dark Strong Ale",
"Belgian Dubbel",
"Belgian Golden Strong Ale",
"Belgian Pale Ale",
"Belgian Specialty Ale",
"Belgian Tripel",
"Berliner Weisse",
"Best Bitter",
"Bière de Garde",
"Blonde Ale",
"Bohemian Pilsener",
"Braggot",
"Brett Beer",
"British Brown Ale",
"British Golden Ale",
"British Strong Ale",
"Brown Porter",
"Burton Ale",
"California Common",
"California Common Beer",
"Catharina Sour",
"Classic American Pilsner",
"Classic Rauchbier",
"Classic Style Smoked Beer",
"Clone Beer",
"Common Cider",
"Common Perry",
"Cream Ale",
"Cyser (Apple Melomel)",
"Czech Amber Lager",
"Czech Dark Lager",
"Czech Pale Lager",
"Czech Premium Pale Lager",
"Dark American Lager",
"Dark Mild",
"Doppelbock",
"Dortmunder Export",
"Double IPA",
"Dry Mead",
"Dry Stout",
"Dunkelweizen",
"Dunkles Bock",
"Dunkles Weissbier",
"Düsseldorf Altbier",
"Eisbock",
"English Barleywine",
"English Cider",
"English IPA",
"English Porter",
"Experimental Beer",
"Extra Special/Strong Bitter (ESB)",
"Festbier",
"Flanders Brown Ale/Oud Bruin",
"Flanders Red Ale",
"Foreign Extra Stout",
"French Cider",
"Fruit and Spice Beer",
"Fruit Beer",
"Fruit Cider",
"Fruit Lambic",
"German Helles Exportbier",
"German Leichtbier",
"German Pils",
"German Pilsner (Pils)",
"Gose",
"Gueuze",
"Helles Bock",
"Holiday/Winter Special Spiced Beer",
"Imperial IPA",
"Imperial Stout",
"International Amber Lager",
"International Dark Lager",
"International Pale Lager",
"Irish Extra Stout",
"Irish Red Ale",
"Irish Stout",
"Kellerbier: Amber Kellerbier",
"Kellerbier: Pale Kellerbier",
"Kentucky Common",
"Kölsch",
"Lambic",
"Lichtenhainer",
"Light American Lager",
"London Brown Ale",
"Maibock/Helles Bock",
"Märzen",
"Metheglin",
"Mild",
"Mixed-Fermentation Sour Beer",
"Mixed-Style Beer",
"Munich Dunkel",
"Munich Helles",
"New England Cider",
"New Zealand Pilsner",
"No Profile Selected",
"North German Altbier",
"Northern English Brown",
"Oatmeal Stout",
"Oktoberfest/Märzen",
"Old Ale",
"Open Category Mead",
"Ordinary Bitter",
"Other Fruit Melomel",
"Other Smoked Beer",
"Other Specialty Cider or Perry",
"Oud Bruin",
"Piwo Grodziskie",
"Pre-Prohibition Lager",
"Pre-Prohibition Porter",
"Premium American Lager",
"Pyment (Grape Melomel)",
"Rauchbier",
"Robust Porter",
"Roggenbier",
"Roggenbier (German Rye Beer)",
"Russian Imperial Stout",
"Sahti",
"Saison",
"Schwarzbier",
"Scottish Export",
"Scottish Export 80/-",
"Scottish Heavy",
"Scottish Heavy 70/-",
"Scottish Light",
"Scottish Light 60/-",
"Semi-Sweet Mead",
"Southern English Brown",
"Special/Best/Premium Bitter",
"Specialty Beer",
"Specialty Fruit Beer",
"Specialty IPA: Belgian IPA",
"Specialty IPA: Black IPA",
"Specialty IPA: Brown IPA",
"Specialty IPA: New England IPA",
"Specialty IPA: Red IPA",
"Specialty IPA: Rye IPA",
"Specialty IPA: White IPA",
"Specialty Smoked Beer",
"Specialty Wood-Aged Beer",
"Spice, Herb, or Vegetable Beer",
"Standard American Lager",
"Standard/Ordinary Bitter",
"Straight (Unblended) Lambic",
"Strong Bitter",
"Strong Scotch Ale",
"Sweet Mead",
"Sweet Stout",
"Traditional Bock",
"Traditional Perry",
"Trappist Single",
"Tropical Stout",
"Vienna Lager",
"Wee Heavy",
"Weissbier",
"Weizen/Weissbier",
"Weizenbock",
"Wheatwine",
"Wild Specialty Beer",
"Winter Seasonal Beer",
"Witbier",
"Wood-Aged Beer"
            };

            var beerStyles = new List<Beerstyle>();
            for (var i = 0; i < styleNames.Count; i++)
            {
                beerStyles.Add(new Beerstyle()
                {
                    Name = styleNames[i]
                });
            }
            context.Beerstyle.AddRange(beerStyles);
            context.SaveChanges();

            var breweries = new List<Brewery>()
            {
                new Brewery()
                {
                    Name = "Nøgne Ø (Hansa Borg)",
                    Country = "Norway",
                    Website = "https://www.nogne-o.com/"
                },
                new Brewery()
                {
                    Name = "Brasserie de la Senne",
                    Country = "Belgium",
                    Website = "http://brasseriedelasenne.be/"
                }
            };
            context.Brewery.AddRange(breweries);
            context.SaveChanges();

            var beers = new List<Beer>()
            {
                new Beer()
                {
                    ABV = 8.5f,
                    BreweryId = 1,
                    Name = "Nøgne Ø Two Captains Double IPA",
                    RateBeerLink = "https://www.ratebeer.com/beer/nogne-o-two-captains-double-ipa/125773",
                    BeerStyleId = 1
                },
                new Beer()
                {
                    ABV = 9.0f,
                    BreweryId = 1,
                    Name = "Nøgne Ø Imperial Stout",
                    RateBeerLink = "https://www.ratebeer.com/beer/nogne-o-imperial-stout/49092/",
                    BeerStyleId = 2
                },
                new Beer()
                {
                    ABV = 6.3f,
                    BreweryId = 2,
                    Name = "Two Roads Brothers In Farms",
                    RateBeerLink = "https://www.ratebeer.com/beer/de-la-senne-two-roads-brothers-in-farms/535109/",
                    BeerStyleId = 3
                },
                new Beer()
                {
                    ABV = 5.5f,
                    BreweryId = 2,
                    Name = "Betchard Blonde (2006)",
                    RateBeerLink = "https://www.ratebeer.com/beer/betchard-blonde-2006/55996/",
                    BeerStyleId = 4
                },
                new Beer()
                {
                    ABV = 10.0f,
                    BreweryId = 2,
                    Name = "Liquid Riot Jambe d’Érable",
                    RateBeerLink = "https://www.ratebeer.com/beer/de-la-senne-liquid-riot-jambe-d%E2%80%99erable/658854/",
                    BeerStyleId = 15
                }
            };
            context.Beer.AddRange(beers);
            context.SaveChanges();

            var tastings = new List<Tasting>()
            {
                new Tasting()
                {
                    Name = "Bouvet juleølsmaking 2017",
                    Description = "Årlig juleølsmaking for Bouvet Nord",
                    IsActive = false,
                    IsBlind = false
                },
                new Tasting()
                {
                    Name = "Bouvet juleølsmaking 2018",
                    Description = "Årlig juleølsmaking for Bouvet Nord",
                    IsActive = true,
                    IsBlind = false
                },
                new Tasting()
                {
                    Name = "Bouvet blinde juleølsmaking 2018",
                    Description = "Årlig juleølsmaking for Bouvet Nord",
                    IsActive = true,
                    IsBlind = true
                }
            };
            context.Tasting.AddRange(tastings);
            context.SaveChanges();

            var tastingBeers = new List<TastingBeer>()
            {
                new TastingBeer()
                {
                    BeerId = 1,
                    TastingId = 2,
                    SortOrder = 1
                },
                new TastingBeer()
                {
                    BeerId = 2,
                    TastingId = 2,
                    SortOrder = 2
                },
                new TastingBeer()
                {
                    BeerId = 3,
                    TastingId = 2,
                    SortOrder = 3
                },
                new TastingBeer()
                {
                    BeerId = 4,
                    TastingId = 2,
                    SortOrder = 5
                },
                new TastingBeer()
                {
                    BeerId = 5,
                    TastingId = 2,
                    SortOrder = 4
                }

            };
            context.TastingBeer.AddRange(tastingBeers);
            context.SaveChanges();
            
            var tasterAdmin = new Taster()
            {
                DisplayName = "Andreas",
                EmailAddress = "andreas.skaret@bouvet.no",
                Password = "123",
                IsAdmin = true
            };

            var tasterNotAdmin = new Taster()
            {
                DisplayName = "Andreas",
                EmailAddress = "andreas.skaret@gmail.com",
                Password = "123",
                IsAdmin = false
            };
            context.Taster.Add(tasterAdmin);
            context.Taster.Add(tasterNotAdmin);
            context.SaveChanges();
        }
    }
}
