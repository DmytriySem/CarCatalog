using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AuxiliaryClass.Auxiliary;

namespace DAL.EF
{
    public class CarsCatalog : DbContext
    {
        static CarsCatalog()
        {
            Database.SetInitializer(new CatalogDBInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Brand>()
                .HasMany<Model>(c => c.Models)
                .WithRequired(x => x.Brand)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Model>()
                .HasMany<Car>(c => c.Cars)
                .WithRequired(x => x.Model)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Car>()
                .HasMany<Cost>(c => c.Prices)
                .WithRequired(x => x.Car)
                .WillCascadeOnDelete(true);
        }

        public CarsCatalog() : base("name=CarsCatalog") { }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Cost> Prices { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
    }

    public class CatalogDBInitializer : DropCreateDatabaseIfModelChanges<CarsCatalog>// DropCreateDatabaseAlways<CarsCatalog>
    {
        private readonly string[] brands = { "audi", "bmw", "mercedes_benz", "mini", "volkswagen" };
        private const int brandsCount = 5;
        private readonly string[][] models = new string[brandsCount][];
        private readonly string path = "C:/Users/Dev/source/repos/CarsSite/DAL/Images/";
        private readonly string[] descriptionCarWords = { "dynamic", "economical", "functional", "safe", "elegant",
            "luxury", "sporty", "extreme", "ultimate", "ultra", "automatic", "top-level", "agile", "front-wheel drive",
            "expensive", "astonishing", "limitless"};
        private readonly Random rand;

        private const int MIN_CARS_LIST = 10; //2;
        private const int MAX_CARS_LIST = 20; //10;

        private const int MIN_COSTS_LIST = 2;
        private const int MAX_COSTS_LIST = 10;

        private const int MIN_DESCR_CAR_LIST = 2;
        private const int MAX_DESCR_CAR_LIST = 7;

        private const int MIN_CAR_YEAR = 1995;
        private const int MAX_CAR_YEAR = 2018;

        private const int FIRST_MONTH = 1;
        private const int LAST_MONTH = 12;

        private const double MIN_PRICE = 2000.00;
        private const double MAX_PRICE = 20000.00;

        public CatalogDBInitializer()
        {
            models[0] = new string[2] { "A3", "A6" };
            models[1] = new string[2] { "i3", "X7" };
            models[2] = new string[2] { "Vito", "Sprinter" };
            models[3] = new string[3] { "Cabrio", "Clubman", "Roadster" };
            models[4] = new string[5] { "Jetta", "Golf", "Polo", "Transporter", "Touareg" };

            rand = new Random();
        }

        private COLOR getRandomColor()
        {
            int length = Enum.GetNames(typeof(COLOR)).Length;

            return (COLOR)rand.Next(0, length);
        }

        private float getRandomVolumeEngine()
        {
            return VolumeEngine[rand.Next(0, VolumeEngine.Length - 1)];
        }

        private DateTime getRandomDate()
        {
            int year = rand.Next(MIN_CAR_YEAR, MAX_CAR_YEAR);
            int month = rand.Next(FIRST_MONTH, LAST_MONTH);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int day = rand.Next(1, daysInMonth);

            return new DateTime(year, month, day);
        }

        private decimal getRandomPrice()
        {
            return (decimal)(rand.NextDouble()*(MAX_PRICE - MIN_PRICE) + MIN_PRICE);
        }

        private string getRandomDescription()
        {
            string description = String.Empty;            

            for (int f = 0; f < rand.Next(MIN_DESCR_CAR_LIST, MAX_DESCR_CAR_LIST);)
            {
                string word = descriptionCarWords[rand.Next(0, descriptionCarWords.Length - 1)];

                if (description.Contains(word))
                {
                    continue;
                }
                else
                {
                    description += word + ", ";
                    f++;
                }
            }

            return description.Substring(0, description.Length - 2);
        }

        protected override void Seed(CarsCatalog context)
        {
            for (int i = 0; i < brands.Length; i++)
            {
                byte[] image = System.IO.File.ReadAllBytes(path + "Brands" + "/" + brands[i] + ".png");
                Brand brand = new Brand
                {
                    Name = brands[i],
                    Photo = image
                };

                IList<Model> modelList = new List<Model>();
                for (int j = 0; j < models[i].Length; j++)
                {
                    Model model = new Model
                    {
                        Name = models[i][j],
                        PhotoUrl = models[i][j] + ".jpg",
                        Brand = brand,
                        BrandId = brand.Id
                    };
                    modelList.Add(model);

                    IList<Car> carList = new List<Car>();
                    for (int l = 0; l < rand.Next(MIN_CARS_LIST, MAX_CARS_LIST); l++)
                    {
                        Car car = new Car()
                        {
                            BrandId = i,
                            Brand = brand,
                            Color = getRandomColor(),
                            VolumeEngine = Math.Round(getRandomVolumeEngine(), 2),
                            Description = getRandomDescription(),
                            ModelId = j,
                            Model = model
                        };

                        IList<Cost> costList = new List<Cost>();
                        for (int m = 1; m < rand.Next(MIN_COSTS_LIST, MAX_COSTS_LIST); m++)
                        {
                            Cost cost = new Cost()
                            {
                                Date = getRandomDate(),
                                Price = getRandomPrice(),
                                CarId = l,
                                Car = car
                            };
                            costList.Add(cost);

                            car.Prices.Add(cost);
                        }
                        context.Prices.AddRange(costList);

                        carList.Add(car);
                    }   
                    context.Cars.AddRange(carList);
                }
                context.Brands.Add(brand);
                context.Models.AddRange(modelList);
            }

            context.SaveChanges();
        }
    }
}
