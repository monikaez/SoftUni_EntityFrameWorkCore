namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using System.Text;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            // DbInitializer.ResetDatabase(db);
            //2
            //string input = Console.ReadLine();
            //Console.WriteLine(GetBooksByAgeRestriction(db,input));
            //3 
            //Console.WriteLine(GetGoldenBooks(db));
            //4
            //Console.WriteLine(GetBooksByPrice(db));
            //5
            //string input = Console.ReadLine();
            //int inputInt = Int32.Parse(input);
            //Console.WriteLine(GetBooksNotReleasedIn(db,inputInt));
            //6
            //string input = Console.ReadLine();
            //Console.WriteLine(GetBooksByCategory(db, input));
            //7
            //string date = Console.ReadLine();
            // Console.WriteLine(GetBooksReleasedBefore(db, date));
            //8
            //string input = Console.ReadLine();
            // Console.WriteLine(GetAuthorNamesEndingIn(db, input));
            //9
            //string input = Console.ReadLine();
            //Console.WriteLine(GetBookTitlesContaining(db, input));
            //10
            //string input = Console.ReadLine();
            //Console.WriteLine(GetBooksByAuthor(db, input));
            //11.CountBooks
            //string input = Console.ReadLine();
            //Console.WriteLine(CountBooks(db,Int32.Parse(input)));
            //12
            //Console.WriteLine(CountCopiesByAuthor(db));
            //13
            //Console.WriteLine(GetTotalProfitByCategory(db));
            //14
            //Console.WriteLine(GetMostRecentBooks(db));
            //15
            // IncreasePrices(db);
            // 16


        }
        //2. Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            if (!Enum.TryParse<AgeRestriction>(command, true, out var ageRestriction))
            {
                return $"{command} is not valid age restriction!";
            }

            var books = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => new
                {
                    b.Title
                })
                .OrderBy(b => b.Title)
                .ToList();

            // Console.WriteLine(books.ToQueryString());
            return string.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        //3.Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        //4.Books by Price
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title} - ${b.Price:f2}"));

        }

        //5.Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Select(b => new
                {
                    b.Title,
                    b.ReleaseDate
                })
                .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year != year)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        //6.Book Titles by Category
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToArray();

            var books = context.Books
                .Select(b => new
                {
                    b.Title,
                    b.BookCategories
                })
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        //7.Released Before Date
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var parseDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate < parseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                    b.ReleaseDate
                })
                .OrderByDescending(b => b.ReleaseDate);
            //If I Forget Thee Jerusalem - Gold - $33.21
            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}"));
        }
        //8.Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName
                })
                .OrderBy(a => a.FullName);
            //George Powell
            return string.Join(Environment.NewLine, authors.Select(a => a.FullName));
        }

        //9.Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            //Finding the Books
            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();

            //Output
            return String.Join(Environment.NewLine, books);
        }

        //10.Book Search by Author
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            //Finding the Books
            var books = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .Select(b => new
                {
                    b.Title,
                    AuthorName = b.Author.FirstName + " " + b.Author.LastName,
                    b.BookId
                })
                .OrderBy(b => b.BookId)
                .ToArray();

            //Output
            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.AuthorName})");
            }
            return sb.ToString().TrimEnd();
        }

        //11.CountBooks
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books
                .Count(b => b.Title.Length > lengthCheck);
        }

        //12.Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    AuthorsFullName = string.Join(" ", a.FirstName, a.LastName),
                    TotalBookByAuthor = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.TotalBookByAuthor)
                .ToList();

            return string.Join(Environment.NewLine, authors.Select(a => $"{a.AuthorsFullName} - {a.TotalBookByAuthor}"));
        }

        //13.Profit by Category
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var profitByCategory = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TotalProfit = c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price)
                })
                .OrderByDescending(cb => cb.TotalProfit)
                .ThenBy(c => c.CategoryName)
                .ToList();

            return string.Join(Environment.NewLine, profitByCategory.Select(pc => $"{pc.CategoryName} ${pc.TotalProfit:f2}"
            ));
        }

        //14.Most Recent Books
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var booksAndCategories = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    MostRecetBooks = c.CategoryBooks.OrderByDescending(bc => bc.Book.ReleaseDate)
                    .Take(3)
                    .Select(cb => new
                    {
                        BookTitle = cb.Book.Title,
                        cb.Book.ReleaseDate.Value.Year
                    })
                })
                .OrderBy(c => c.CategoryName);

            StringBuilder sb = new();

            foreach (var category in booksAndCategories)
            {
                sb.AppendLine($"--{category.CategoryName}");
                foreach (var book in category.MostRecetBooks)
                {
                    sb.AppendLine($"{book.BookTitle} ({book.Year})");
                }
            }
            return sb.ToString().TrimEnd();
        }

        //15.Increase Prices
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year < 2010);

            foreach (var book in books)
            {
                book.Price += 5;
            }
            context.SaveChanges();
        }

        //16.Remove Books

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200);

            context.RemoveRange(books);
            context.SaveChanges();

            return books.Count();
        }
    }
}


