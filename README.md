# Vidly
An ASP.NET MVC demo project from
[The Complete ASP.NET MVC 5 Course](https://www.udemy.com/the-complete-aspnet-mvc-5-course/)
by [Mosh Hamedani ](https://www.udemy.com/user/moshfeghhamedani/)

*Enroll to the course for excellent detailed explanation on everything crucial.*


&nbsp;
## 00 Start the project
* In VS start a new project named Vidly. Select *ASP.NET Web Application*. Use the *MVC* template.

&nbsp;
## 01 Add the *Movie* class

* In *Models* add the *Movie* class.

* Add the *Id* and *Name* properties.

```
public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
```

&nbsp;
## 02 Add the *MoviesController* controller and the *Random* movies view

* In *Controllers* add the empty *MoviesController*.

* Create the *Random* action. It contains an instance of the *Movie* model for the movie named "Shrek!" for demonstration.

```
    // GET: Movies/Random
    public ActionResult Random()
    {
        var movie = new Movie() { Name = "Shrek!" };
        return View(movie);
    }
```

* In *Views/Movies* add the *Random* view. It uses the *Shared/_Layout.cshtml*. Add the model defining directive and render the movie's *Name* property.

```
@model Vidly.Models.Movie

@{
    ViewBag.Title = "Random";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Model.Name</h2>
```

&nbsp;
## 03 Add the *Lumen* Bootstrap theme

* Get the *Lumen* theme css code from Bootswatch.com.

* Add it in *Content* as *bootstrap-lumen.css*.

* Edit *App_Start/BundleConfig.cs* in order to set the theme to *Lumen*.

```
bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-lumen.css",
                      "~/Content/site.css"));
```                      

&nbsp;
## 04 Add a partial view for the NavBar

* In *Shared* add the *_NavBar* partial view.

* Cut the NavBar code from *_Layout.cshtml* and paste it into *_Navbar.cshtml*.

* In place of the cut code in *_Layout.cshtml* add a call to the *Html.Partial* method.

```
@Html.Partial("_NavBar")
```


&nbsp;
## 05 Edit app name and nav links

* Change the app name to *Vidly* in the footer section of *_Layout.cshtml*.

```
<footer>
    <p>&copy; @DateTime.Now.Year - Vidly</p>
</footer>
```

* Change the app name to *Vidly* inside *_Navbar.cshtml*.

```
@Html.ActionLink("Vidly", "Index", "Home", new { area = "" }, new { @class = "navbar-brand" })
```

* In *_Navbar.cshtml* remove the existing links and add links for *Customers* and *Movies*.

```
<li>@Html.ActionLink("Customers", "Index", "Customers")</li>                
<li>@Html.ActionLink("Movies", "Index", "Movies")</li>
```


&nbsp;
## 06 Edit *MoviesController*, add *Index* and *Getmovies*

* Edit *MoviesController* and add the *Index* class, which is calling the *GetMovies* private method.  *Index* returns a *ViewResult* action result, which gets the return of *GetMovies* as an input parameter. *GetMovies* is defined as an `IEnumerable<Movie>` interface that returns a list of available movies.
```
    public ViewResult Index()
    {
        var movies = GetMovies();

        return View(movies);
    }

    private IEnumerable<Movie> GetMovies()
    {
        return new List<Movie>
        {
            new Movie { Id = 1, Name = "Shrek" },
            new Movie { Id = 2, Name = "Wall-e" }
        };
    }
```

&nbsp;
## 07 Add the *Movies/Index* view

* In *Views/Movies* add the *Index* view, using the *~/Views/Shared/_Layout.cshtml* layout.

* Configure the view so that it displays the *Name* property of each movie in the `IEnumerable<Vidly.Models.Movie>` model.
```
<h2>Movies</h2>
<table class="table table-bordered table-hover">
    <thead>
        <tr>
            <th>Movie</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var movie in Model)
        {
            <tr>
                <td>@movie.Name</td>
            </tr>
        }
    </tbody>
</table>
```

&nbsp;
## 08 Add the *Customer* class and controller

* In *Models*, add the *Customer* class and give it an *Id* and a *Name* properties.

```
public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
```

* In *Controllers*, add the *CustomersController* controller. It contains the *Index* class, which is calling the *GetCustomers* private method.  *Index* returns a *ViewResult* action result, which gets the return of *GetCustomers* as an input parameter. *GetCustomers* is defined as an `IEnumerable<Customer>` interface that returns a list of available customers.

```
public class CustomersController : Controller
    {
        public ViewResult Index()
        {
            var customers = GetCustomers();

            return View(customers);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "John Smith" },
                new Customer { Id = 2, Name = "Mary Williams" }
            };
        }
    }
```

&nbsp;
## 09 Add the *Customers/Index* view

* In *Views/Customers* add the *Index* view, using the *~/Views/Shared/_Layout.cshtml* layout.

* Configure the view so that it displays the *Name* property of each customer in the `IEnumerable<Vidly.Models.Customer>` model. If there are not customers an appropriate message is returned.

```
<h2>Customers</h2>
@if (!Model.Any())
{
    <p>There are no customers yet.</p>
}
else
{
    <table class="table table-bordered table-hover">
        <thead>
            <tr>
                <th>Customer</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var customer in Model)
            {
                <tr>
                    <td>@customer.Name</td>
                </tr>
            }
        </tbody>
    </table>
}
```

&nbsp;
## 10 Add *Details* for customers

* In the *CustomersController*, add the *Details* method. It returns an *ActionResult* which provides a view of a single customer, whose Id matches the method's input parameter.
```
    public ActionResult Details(int id)
    {
        var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

        if (customer == null)
            return HttpNotFound();

        return View(customer);
    }
```

* In *Views/Customers/Index* replace the code that displays each customer's *Name* with an *ActionLink* which displays the *customer.Name*, invokes the *Details* method from the *Customers* controller and assigns the *customer.Id* on the method's *id* argument.

```
<td>@Html.ActionLink(customer.Name, "Details", "Customers", new { id = customer.Id }, null)</td>
```

* In *Views/Customers* add the *Details* view, using the *~/Views/Shared/_Layout.cshtml* layout. Set the model to *Vidly.Models.Customer*, the title to the *Model.Name* and display the *Model.Name*.

```
@model Vidly.Models.Customer

@{
    ViewBag.Title = Model.Name;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Model.Name</h2>
```


&nbsp;
## 11 Add *Details* for movies

* In the *MoviesController*, add the *Details* method. It returns an *ActionResult* which provides a view of a single movie, whose Id matches the method's input parameter.

```
public ActionResult Details(int id)
{
      var movie = GetMovies().SingleOrDefault(m => m.Id == id);

      if (movie == null)
          return HttpNotFound();

      return View(movie);
}
```

* In *Views/Movies/Index* replace the code that displays each movie's *Name* with an *ActionLink* which displays the *movie.Name*, invokes the *Details* method from the *Movies* controller and assigns the *movie.Id* on the method's *id* argument.

```
<td>@Html.ActionLink(movie.Name, "Details", "Movies", new { id = movie.Id }, null)</td>
```

* In *Views/Movies* add the *Details* view, using the *~/Views/Shared/_Layout.cshtml* layout. Set the model to *Vidly.Models.Movie*, the title to the *Model.Name* and display the *Model.Id* and the *Model.Name*.

```
@model Vidly.Models.Movie

@{
    ViewBag.Title = Model.Name;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h3>Details for movie <b>@Model.Name</b></h3>
<p>Id: @Model.Id</p>
<p>Name: @Model.Name</p>
```


## 12 Enable migrations

```
PM> enable-migrations
```

```
PM> add-migration InitialModel
```

```
PM> update-database
```

* In the created migration we can see calls to the CreateTable method for tables referring to users and roles, coming from the ASP.NET Identity framework.
Inside IdentityModels.cs, the ApplicationDbContext class inherits from IdentityDbContext. Entity framework located the auto generated Identity DbSets while creating the migration. No references to our domain classes (Movie, Customer) are found.


&nbsp;
## 13 Add the DbSet for *Customers*

* Inside IdentityModels.cs, in ApplicationDbContext, add the DbSet for *Customers*.

```
public DbSet<Customer> Customers { get; set; }
```

```
PM> Add-migration AddCustomersTable
```
```
PM> update-database
```


&nbsp;
## 14 Add a property to Customer

* Add the IsSubscribedToNewsletter property to the Customer model.

```
public bool IsSubscribedToNewsletter { get; set; }
```

```
PM> add-migration AddIsSubscribedToNewsLetterToCustomer
```
```
PM> update-database
```


&nbsp;
## 15 Add the MembershipType model

* Add the MembershipType class in the models folder.

```
    public class MembershipType
    {
        public byte Id { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }
    }
```

* MembershipType has to be associated with the Customer class. Add the MembershipType navigational property to the Customer class as well as the MembershipTypeId property which is recognized by convention by the Entity framework as a foreign key.

```
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }
    }
```

```
PM> add-migration AddMembershipType
```
```
PM> update-database
```


&nbsp;
## 16 Populate MembershipTypes with a migration

* Create an empty migration.

```
PM> add-migration PopulateMembershipTypes
```
```
public partial class PopulateMembershipTypes : DbMigration
    {
        public override void Up()
        {
        }

        public override void Down()
        {
        }
    }
```

* Then call the *Sql* method and type in SQL statements.

```
public override void Up()
{
    Sql("INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate) VALUES (1, 0, 0, 0)");
    Sql("INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate) VALUES (2, 30, 1, 10)");
    Sql("INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate) VALUES (3, 90, 3, 15)");
    Sql("INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate) VALUES (4, 300, 12, 20)");
}
```

* By updating, we can seed the MembershipTypes data to the database.

```
PM> update-database
```


&nbsp;
## 17 Override conventions with data annotations

* In the Customer model, apply attributes with DataAnnotations in order to make Name not nullable and set its string length to 255.

```
using System.ComponentModel.DataAnnotations;
```

```
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }
    }
```

```
PM> add-migration ApplyAnnotationsToCustomerName
```
```
PM> update-database
```


&nbsp;
## 18 Get customers from a database


* In Web.config, edit the connection string in order to point to the database of our choice.

```
<connectionStrings>    
  <add name="DefaultConnection"
    connectionString="Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Vidly20171202;Integrated Security=SSPI"
    providerName="System.Data.SqlClient" />
</connectionStrings>
```


* In CustomersController.cs, declare a dbContext in order to access the database. Add a constructor (ctor +Tab) and initialize the dbContext inside it. Override the dispose method of the base controller class and call Dispose() on the dbContext.

```
    private ApplicationDbContext _context;

    public CustomersController()
    {
        _context = new ApplicationDbContext();
    }

    protected override void Dispose(bool disposing)
    {
        _context.Dispose();
    }
```


* Instead of calling the *GetCustomers* method, initialize the *customers* variable using the dbContext's DbSet *Customers*. Force immediate execution of the query by calling the *ToList* method.

```
    public ViewResult Index()
    {
        var customers = _context.Customers.ToList();

        return View(customers);
    }
```


* In the *Details* action, we must also replace the *GetCustomers* method and get the selected customer from the database by use of the dbContext.

```
    public ActionResult Details(int id)
    {
        var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

        if (customer == null)
            return HttpNotFound();

        return View(customer);
    }
```


* Run the project. The database declared in Web.config (Vidly20171202) will be created. If we browse the Customers tab, we see that there are no customers in the database, as dictated by the *@if (!Model.Any())* condition in the Views/Customers/Index.cshtml view.

```
There are no customers yet.
```


* Add some customer data manually since these are not reference data (unlike the MembershipType data we seeded previously).

* Now the customer index and detail pages can load customer data from the database. The movie index and detail still receive data created with the GetMovies helper seed method.



&nbsp;
## 19 Eager load customer membership type

* Edit the *Views/Customers/Index.cshtml* in order to display the customers' discount rate, which is described in the MembershipType DbSet.

```
<table class="table table-bordered table-hover">
  <thead>
      <tr>
          <th>Customer</th>
          <th>Discount Rate</th>
      </tr>
  </thead>
  <tbody>
      @foreach (var customer in Model)
      {
          <tr>
              <td>@Html.ActionLink(customer.Name, "Details", "Customers", new { id = customer.Id }, null)</td>
              <td>@customer.MembershipType.DiscountRate%</td>
          </tr>
      }
  </tbody>
</table>
```


* If we attempt to run this page we will get a null reference exception error because the MembershipType object is null. By default Entity framework loads only the declared objects and not their related objects.

* Use the *Include* method in the Index action in CustomersController.cs in order to enable Eager Loading for the Customers and their MembershipTypes.

```
using System.Data.Entity;
```

```
    public ViewResult Index()
    {
        var customers = _context.Customers.Include(c => c.MembershipType).ToList();

        return View(customers);
    }
```


&nbsp;
## 20 Apply names to MembershipTypes

* Edit the MembershipType model. Add the Name property. Make it non nullable.

```
using System.ComponentModel.DataAnnotations;
```

```
    public class MembershipType
    {
        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }
    }
```

* Apply the migration.
```
PM> add-migration AddNameToMembershipType
```
```
PM> update-database
```

* Apply another migration in order to update the database entries with the appropriate names.

```
PM> add-migration SetNamesOfMembershipTypes
```

* In the empty migration, use the Sql command on order to apply SQL update commands and set the names.

```
public override void Up()
{
    Sql("UPDATE MembershipTypes SET Name = 'Pay as You Go' WHERE Id = 1");
    Sql("UPDATE MembershipTypes SET Name = 'Monthly' WHERE Id = 2");
    Sql("UPDATE MembershipTypes SET Name = 'Quarterly' WHERE Id = 3");
    Sql("UPDATE MembershipTypes SET Name = 'Annual' WHERE Id = 4");
}
```
```
PM> update-database
```

* Edit the Views/Customers/Index.cshtml in order to display the customers' membership type name.

```
<table class="table table-bordered table-hover">
  <thead>
      <tr>
          <th>Customer</th>
          <th>Membership Type</th>
          <th>Discount Rate</th>
      </tr>
  </thead>
  <tbody>
      @foreach (var customer in Model)
      {
          <tr>
              <td>@Html.ActionLink(customer.Name, "Details", "Customers", new { id = customer.Id }, null)</td>
              <td>@customer.MembershipType.Name</td>
              <td>@customer.MembershipType.DiscountRate%</td>
          </tr>
      }
  </tbody>
</table>
```


&nbsp;
## 21 Add Birthdate to customer data

* Edit the Customer model. Add the nullable Birthdate property.

```
public DateTime? Birthdate { get; set; }
```


* Apply the migration.

```
PM> add-migration AddBirthdateToCustomer
```
```
PM> update-database
```

* Edit manually the birthdates of some customers in the database.

* Use the *Include* method in the Details action in CustomersController.cs in order to enable Eager Loading for the Customers and their MembershipTypes.

```
using System.Data.Entity;
```
```
public ActionResult Details(int id)
{
    var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

    if (customer == null)
        return HttpNotFound();

    return View(customer);
}
```

* Update the Views/Customers/Details.cshtml in order to display the customer's membership type and the birthdate whenever it is available.

```
@model Vidly.Models.Customer

@{
    ViewBag.Title = Model.Name;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Model.Name</h2>
<ul>
    <li>Membership Type: @Model.MembershipType.Name</li>
    @if (Model.Birthdate.HasValue)
    {
        <li>Birthdate: @Model.Birthdate.Value.ToShortDateString()</li>
    }
</ul>
```

&nbsp;
## 22 Add the movies table in the database

* Inside IdentityModels.cs, in ApplicationDbContext, add the DbSet for *Movies*.

```
    public DbSet<Movie> Movies { get; set; }
```

```
PM> add-migration AddMoviesTable
```

```
PM> update-database
```


&nbsp;
## 23 Add genres

* In models, add the *Genre* class.

```
using System.ComponentModel.DataAnnotations;
```
```
    public class Genre
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
```

* Inside IdentityModels.cs, in ApplicationDbContext, add the DbSet for *Genres*.

```
    public DbSet<Genre> Genres { get; set; }
```

```
PM> add-migration AddGenresTable
```

```
PM> update-database
```


* Genre contains reference data, so we will populate the database with a migration.

```
PM> add-migration PopulateGenreTypes
```

* Before updating the database, create SQL insert statements for the genre types by use of the Sql method in the empty migration.

```
public partial class PopulateGenreTypes : DbMigration
{
    public override void Up()
    {
        Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Action')");
        Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Romance')");
        Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Thriller')");
        Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Comedy')");
        Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'Crime')");
        Sql("INSERT INTO Genres (Id, Name) VALUES (6, 'Horror')");
        Sql("INSERT INTO Genres (Id, Name) VALUES (7, 'Western')");
    }
}
```

```
PM> update-database
```


&nbsp;
## 24 Update the Movie model

* Edit the Movie model. Add properties for date added, release date, number of copies in stock. Genre has to be associated with the Movies class. Add the Genre navigational property to the Movie class. Also add the GenreId property which is recognized by convention by the Entity framework as a foreign key.

```
using System.ComponentModel.DataAnnotations;
```

```
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime ReleaseDate { get; set; }

        public byte NumberInStock { get; set; }

        [Required]
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
    }
```

```
PM> add-migration UpdateTheMovieModel
```
```
PM> update-database
```


&nbsp;
## 25 Get movies from the database

* Modify the controller and the views for the movies in order to update the way the movies list and the movie details are presented.

* In MoviesController.cs, declare a dbContext in order to access the database. Add a constructor (ctor +Tab) and initialize the dbContext inside it. Override the dispose method of the base controller class and call Dispose() on the dbContext.

```
    private ApplicationDbContext _context;

    public MoviesController()
    {
        _context = new ApplicationDbContext();
    }

    protected override void Dispose(bool disposing)
    {
        _context.Dispose();
    }
```

* In the *Index* action, instead of calling the *GetMovies* method, initialize the *movies* variable using the dbContext's DbSet *Movies*. Force immediate execution of the query by calling the *ToList* method. Use the *Include* method in order to enable Eager Loading for the Movies and their Genres.

```
using System.Data.Entity;
```
```
    public ViewResult Index()
    {
        var movies = _context.Movies.Include(m => m.Genre).ToList();

        return View(movies);
    }
```


* In the *Details* action, we must also replace the *GetMovies* method and get the selected movie from the database by use of the dbContext. Eager loading has to be applied here with *Include* as well.

```
var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

```

* Now the movies index and detail pages are loading the data from the database. Add demo data for movies to the database manually.

* Edit the views in order to display the genre in the list of movies and the additional fields in the movie details.

*Views/Movies/Index.cshtml*
```
<thead>
    <tr>
        <th>Movie</th>
        <th>Genre</th>
    </tr>
</thead>
<tbody>
    @foreach (var movie in Model)
    {
        <tr>
            <td>@Html.ActionLink(movie.Name, "Details", "Movies", new { id = movie.Id }, null)</td>
            <td>@movie.Genre.Name</td>
        </tr>
    }
</tbody>
```

*Views/Movies/Details.cshtml*
```
<ul>
    <li>Genre: @Model.Genre.Name</li>
    <li>Release Date: @Model.ReleaseDate.ToLongDateString()</li>
    <li>Date Added: @Model.DateAdded.ToLongDateString()</li>
    <li>Number in Stock: @Model.NumberInStock</li>
</ul>
```


&nbsp;
## 26 Create the New Customer form

* In CustomersController.cs, create the New() action.

```
    public ActionResult New()
    {
        return View();
    }
```

* Create the related view.
  * Use the *BeginForm* *Html* helper method which takes the *Create* action and the *Customers* controller parameters. Wrap it in a using statement in order to create the closing *</Form>* tag.
  * Use bootstrap markup inside.
  * Use the *LabelFor* *Html* method with a lambda expression for the model name.
  * Use the *TextBoxFor* *Html* method with a lambda expression for the model name and an anonymous object for assigning the *form-control* bootstrap class.
  * Add another label and textbox for the customer birthdate. Apply a *Display* data annotation to the Birthdate property in the customer model in order to display the label as "Date of Birth".
  * Add another label and a checkbox for the customer *IsSubscribedToNewsletter* property. *LabelFor* will not be used in this case. The bootstrap *checkbox* class will be assigned to the wrapping div element.

*Views/Customers/New.cshtml*
```
@model Vidly.Models.Customer

@{
    ViewBag.Title = "New";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>New Customer</h2>

@using (Html.BeginForm("Create", "Customers"))
{
    <div class="form-group">
        @Html.LabelFor(m => m.Name)
        @Html.TextBoxFor(m => m.Name, new { @class = "form-control" })
    </div>
    <div class="form-group">
        @Html.LabelFor(m => m.Birthdate)
        @Html.TextBoxFor(m => m.Birthdate, new { @class = "form-control" })
    </div>
    <div class="checkbox">
        <label>
            @Html.CheckBoxFor(m => m.IsSubscribedToNewsletter)
            Subscribed to Newsletter
        </label>        
    </div>
}
```

*Models/Customer.cs*
```
    [Display(Name="Date of Birth")]
    public DateTime? Birthdate { get; set; }
```    


&nbsp;
## 27 Select membership type from a dropdown list

* Inside IdentityModels.cs, in ApplicationDbContext, add the DbSet for *MembershipTypes*.

```
    public DbSet<MembershipType> MembershipTypes { get; set; }
```

* We will need a view model that contains membership types and properties of the customer domain object. Add the *ViewModels* folder and create the *NewCustomerViewModel*. Get a list of membership types by use of an IEnumerable. Add the Customer object (the scale of the project is such that there is no necessity to add properties individually) in order to access its required by the view properties.

```
using Vidly.Models;
```
```
namespace Vidly.ViewModels
{
    public class NewCustomerViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }

        public Customer Customer { get; set; }
    }
}
```

* In CustomersController.cs, edit the New() action.  

```
using Vidly.ViewModels;
```
```
    public ActionResult New()
    {
       var membershipTypes = _context.MembershipTypes.ToList();

       var viewModel = new NewCustomerViewModel
       {
           MembershipTypes = membershipTypes
       };

       return View(viewModel);
    }
```

* In the view, update the model used and prefix the customer properties in the lambda expressions with *Customer*. Then add a div with the code for the membership type dropdown list by use of *DropDownListFor* *Html* method.

*Views/Customers/New.cshtml*
```
@model Vidly.ViewModels.NewCustomerViewModel

@{
    ViewBag.Title = "New";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>New Customer</h2>

@using (Html.BeginForm("Create", "Customers"))
{
    <div class="form-group">
        @Html.LabelFor(m => m.Customer.Name)
        @Html.TextBoxFor(m => m.Customer.Name, new { @class = "form-control" })
    </div>
    <div class="form-group">
        @Html.LabelFor(m => m.Customer.Birthdate)
        @Html.TextBoxFor(m => m.Customer.Birthdate, new { @class = "form-control" })
    </div>
    <div class="checkbox">
        <label>
            @Html.CheckBoxFor(m => m.Customer.IsSubscribedToNewsletter)
            Subscribed to Newsletter
        </label>        
    </div>
    <div class="form-group">
        @Html.LabelFor(m => m.Customer.MembershipTypeId)
        @Html.DropDownListFor(m => m.Customer..MembershipTypeId,
                new SelectList(Model.MembershipTypes, "Id", "Name"),
                "Select Membership Type", new { @class = "form-control" } )
    </div>
}
```

* Apply a *Display* data annotation to the MembershipTypeId property in the customer model in order to display the label as "Membership Type".

*Models/Customer.cs*
```
    [Display(Name = "Membership Type")]
    public byte MembershipTypeId { get; set; }
```



&nbsp;
## 28 Add the *Save* button and action

* Add a button to the view. Set the type to *submit* and add bootstrap classes.

```
    <button type="submit" class="btn btn-primary">Save</button>
```


* In CustomersController.cs, add the Save() action.  

  * Apply the *HttpPost* attribute to make sure that the action is called only with the POST HTTP request method.

  * **Model binding**: Add a parameter of *Customer* type. MVC framework binds the customer model to the request data.

  * Check whether the customer is a new or an existing one.

  * **Add to context**: If Customer.Id is zero, then create a new customer. Add the customer that we will create to the dbContext by use of the *Add* method.

  * If Customer.Id is not zero, we have an existing customer. We get the customer from the database and update the data by setting the properties of the Customer object.

  * **Persist changes**: Add, modify or delete changes are persisted by use of the *SaveChanges* method on the context.

  * Redirect the user to the list of customers by returning a *RedirectToAction* method.

  ```
      [HttpPost]
      public ActionResult Save(Customer customer)
      {
          if (customer.Id == 0)
              _context.Customers.Add(customer);
          else
          {
              var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

              customerInDb.Name = customer.Name;
              customerInDb.Birthdate = customer.Birthdate;
              customerInDb.MembershipTypeId = customer.MembershipTypeId;
              customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
          }

          _context.SaveChanges();

          return RedirectToAction("Index", "Customers");
      }
  ```

* The customer Id must be provided in the form.



&nbsp;
## 29 Refactor

* The new customer form will be also used for updating existing customers' data. Refactoring is required in order to adjust to the newly introduced functionality.

  * Rename the view model from *NewCustomerViewModel* to *CustomerFormViewModel*.

  * Rename the *Views/Cuatomers/New.cshtml* view to *CustomerForm.cshtml*.

  * Edit the *New* action in CustomersController.cs.

    * In the viewModel a new Customer has to be initialized.

    * In the return, specify the view name which is now "CustomerForm".

  ```
      public ActionResult New()
      {
          var membershipTypes = _context.MembershipTypes.ToList();

          var viewModel = new CustomerFormViewModel
          {
              Customer = new Customer(),
              MembershipTypes = membershipTypes
          };

          return View("CustomerForm", viewModel);
      }
  ```

* Refactor *CustomerForm.cshtml*.

  * Conditionally render "New Customer" or "Edit Customer".

  * Rename the target action from *Create* to *Save*.

  * Format the Birthdate string.

  * Relocate the checkbox to above the Save button.

  * **Provide the customer Id in the form as a hidden field by use of *HiddenFor*.**


  ```
  @model Vidly.ViewModels.CustomerFormViewModel

  @{
      ViewBag.Title = "CustomerForm";
      Layout = "~/Views/Shared/_Layout.cshtml";
  }

  @if (@Model.Customer.Id == 0)
  { <h2>New Customer</h2> }
  else
  { <h2>Edit Customer</h2> }


  @using (Html.BeginForm("Save", "Customers"))
  {
      <div class="form-group">
          @Html.LabelFor(m => m.Customer.Name)
          @Html.TextBoxFor(m => m.Customer.Name, new { @class = "form-control" })
      </div>
      <div class="form-group">
          @Html.LabelFor(m => m.Customer.Birthdate)
          @Html.TextBoxFor(m => m.Customer.Birthdate, "{0:d MMMM yyyy}", new { @class = "form-control" })
      </div>    
      <div class="form-group">
          @Html.LabelFor(m => m.Customer.MembershipTypeId)
          @Html.DropDownListFor(m => m.Customer.MembershipTypeId,
            new SelectList(Model.MembershipTypes, "Id", "Name"),
             "Select Membership Type", new { @class = "form-control" } )
      </div>
      <div class="checkbox">
          <label>
              @Html.CheckBoxFor(m => m.Customer.IsSubscribedToNewsletter)
              Subscribed to Newsletter
          </label>
      </div>
      @Html.HiddenFor(m => m.Customer.Id)
      <button type="submit" class="btn btn-primary">Save</button>
  }
  ```



&nbsp;
## 30 Edit customers

* In *Views/Customers/Index.cshtml* replace the *Details* action in the customer ActionLink with *Edit*. Now, clicking on a customers' name will navigate to the *CustomerForm* view instead of the *Details* view.

```
<td>@Html.ActionLink(customer.Name, "Edit", "Customers", new { id = customer.Id }, null)</td>
```


* In CustomersController.cs, create the *Edit* action.

  * Select a customer by Id by use of the *SingleOrDefault* method.

  * Define the viewModel and initialize Customer and MembershipTypes.

  * In the return, specify the view name ("CustomerForm").

  ```
      public ActionResult Edit(int id)
      {
          var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
          var membershipTypes = _context.MembershipTypes.ToList();

          if (customer == null)
              return HttpNotFound();

          var viewModel = new CustomerFormViewModel
          {
              Customer = customer,
              MembershipTypes = membershipTypes
          };

          return View("CustomerForm", viewModel);
      }
  ```


&nbsp;
## 31 Add the New Customer button

* In *Views/Customers/Index.cshtml* add an ActionLink navigating to the New action. Style it as a button with bootstrap classes.

```
<p>
  @Html.ActionLink("New Customer", "New", "Customers", null, new { @class = "btn btn-primary" })    
</p>
```

&nbsp;
## 32 Move the New/Edit customer logic to the ViewModel

* In *CustomerForm.cshtml* there is a conditional for the title selection.

```
@if (@Model.Customer.Id == 0)
{ <h2>New Customer</h2> }
else
{ <h2>Edit Customer</h2> }
```

It is better practice to keep logic apart from the views if possible. Add the Title property in *CustomerFormViewModel* and include the logic inside the definition.

```
    public string Title
    {
        get
        {
            if (Customer != null && Customer.Id != 0)
                return "Edit Customer";

            return "New Customer";
        }
    }
```

and replace the conditional in the customer form with the title.

```
<h2>@Model.Title</h2>
```


&nbsp;
## 33 Update the Movie model

* Update the Movie model.

  * In *Movie.cs*, add *Display* attributes in order to display the properties' names properly in the movies form.

  * Make the *ReleaseDate* property nullable.

  * Remove the *Required* attribute from the *Genre* property, since it is only a navigation property, and apply it to *GenreId*.


```
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        public byte NumberInStock { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte GenreId { get; set; }
    }
```


```
PM> add-migration MakeReleaseDateNullable
```
```
PM> update-database
```


&nbsp;
## 34 Add the MovieFormViewModel

* Inside the *ViewModels* folder, add *MovieFormViewModel.cs*. Include properties for *Movie*, *IEnumerable<Genre>* and a *Title* that will implement the New-or-Edit logic.

```
using Vidly.Models;
```

```
   public class MovieFormViewModel
   {
       public IEnumerable<Genre> Genres { get; set; }

       public Movie Movie { get; set; }

       public string Title
       {
           get
           {
               if (Movie != null && Movie.Id != 0)
                   return "Edit Movie";

               return "New Movie";
           }
       }
   }
```


&nbsp;
## 35 Add the MovieForm view

* In *Views/Movies* add the empty *MovieForm* view using the *~/Views/Shared/_Layout.cshtml*.

  * Set the model to MovieFormViewModel.

  * Display the *Model.Title*.

  * Use the *BeginForm* *Html* helper method which takes the *Save* action and the *Movies* controller parameters. Wrap it in a using statement in order to create the closing *</Form>* tag.

  * Use bootstrap markup with the *form-group* class.

  * Use the *LabelFor* and *TextBoxFor* *Html* methods with lambda expressions for the movie name, for the properly formatted *Release* and *Added* dates and for the number in stock.

  * Use the *LabelFor* and *DropDownListFor* *Html* methods for the genre, populated via GenreIds and displaying the genre names with the *SelectList* method.

  * Use anonymous objects for assigning the *form-control* bootstrap class.

  * The *Display* data annotations to the Movie properties in the model will be displayed in the labels.  

  * **Provide the movie Id in the form as a hidden field by use of *HiddenFor*.**

  * Add a button. Set the type to *submit* and add bootstrap classes.

*Views/Movies/MovieForm.cshtml*
```
@model Vidly.ViewModels.MovieFormViewModel

@{
    ViewBag.Title = "MovieForm";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Model.Title</h2>

@using (Html.BeginForm("Save", "Movies"))
{
    <div class="form-group">
        @Html.LabelFor(m => m.Movie.Name)
        @Html.TextBoxFor(m => m.Movie.Name, new { @class = "form-control" })
    </div>
    <div class="form-group">
        @Html.LabelFor(m => m.Movie.DateAdded)
        @Html.TextBoxFor(m => m.Movie.DateAdded, "{0:d MMMM yyyy}", new { @class = "form-control" })
    </div>
    <div class="form-group">
        @Html.LabelFor(m => m.Movie.ReleaseDate)
        @Html.TextBoxFor(m => m.Movie.ReleaseDate, "{0:d MMMM yyyy}", new { @class = "form-control" })
    </div>
    <div class="form-group">
        @Html.LabelFor(m => m.Movie.GenreId)
        @Html.DropDownListFor(m => m.Movie.GenreId,
          new SelectList(Model.Genres, "Id", "Name"), "", new { @class = "form-control" })
    </div>
    <div class="form-group">
        @Html.LabelFor(m => m.Movie.NumberInStock)
        @Html.TextBoxFor(m => m.Movie.NumberInStock, new { @class = "form-control" })
    </div>
    @Html.HiddenFor(m => m.Movie.Id)
    <button type="submit" class="btn btn-primary">Save</button>
}
```


&nbsp;
## 36 Update the movies index view

* Edit *Views/Movies/Index.cshtml*.

  * Add the New Movie button. Add an ActionLink which navigates to the New action. Style it as a button with bootstrap classes.

  * Replace the *Details* action in the movies listing ActionLink with *Edit*. Clicking on a movies' name will be navigating to the *MovieForm* view instead of the *Details* view.

  * Add a table header and related table data in order to display the release year of the movie.

*Views/Movies/Index.cshtml*
```
@model IEnumerable<Vidly.Models.Movie>
@{
    ViewBag.Title = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>Movies</h2>
<p>
    @Html.ActionLink("New Movie", "New", "Movies", null, new { @class = "btn btn-primary" })
</p>
<table class="table table-bordered table-hover">
    <thead>
        <tr>
            <th>Movie</th>
            <th>Release Year</th>
            <th>Genre</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var movie in Model)
        {
        <tr>
            <td>@Html.ActionLink(movie.Name, "Edit", "Movies", new { id = movie.Id }, null)</td>
            <td>
                @if (movie.ReleaseDate != null)
                {
                    @movie.ReleaseDate.Value.Year
                }
            </td>
            <td>@movie.Genre.Name</td>
        </tr>
        }
    </tbody>
</table>
```



&nbsp;
## 37 New Movie and Edit Movie

* Add the *New* action in *MoviesController.cs*.

  * The genres list is being populated by the dbContext, since the *Genres* DbSet has been already defined in *IdentityModels.cs*.

  * A new Movie object will be assigned to the Movie key in the *viewModel* variable. The *DateAdded* value will be autocompleted with the current date.

  * The returned View will be the *MovieForm*.

*Controllers/MoviesController.cs*
```
using Vidly.ViewModels;
```
```
    public ViewResult New()
    {
        var genres = _context.Genres.ToList();

        var viewModel = new MovieFormViewModel
        {
            Movie = new Movie() { DateAdded = DateTime.Now },
            Genres = genres
        };

        return View("MovieForm", viewModel);
    }
```


* Add the *Edit* action in *MoviesController.cs*.

  * Select a movie by Id by use of the *SingleOrDefault* method.

  * Define the viewModel and initialize Movie and Genres.

  * The returned View will be the *MovieForm*.

```
    public ActionResult Edit(int id)
    {
        var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
        var genres = _context.Genres.ToList();

        if (movie == null)
            return HttpNotFound();

        var viewModel = new MovieFormViewModel
        {
            Movie = movie,
            Genres = genres
        };

        return View("MovieForm", viewModel);
    }
```

&nbsp;
## 38 Save movie

* In *MoviesController.cs*, add the Save() action.  

  * Apply the *HttpPost* attribute to make sure that the action is called only with the POST HTTP request method.

  * **Model binding**: Add a parameter of *Movie* type. MVC framework binds the movie model to the request data.

  * Check whether the movie is a new or an existing one.

  * **Add to context**: If Movie.Id is zero, then create a new movie. Add the movie that we will create to the dbContext by use of the *Add* method.

  * If Movie.Id is not zero, we have an existing movie. We get the movie from the database and update the data by setting the properties of the Movie object.

  * Notice that we will not be updating the *DateAdded* property. We will be able to persist the change of this value only when we add a new movie.

  * **Persist changes**: Add, modify or delete changes are persisted by use of the *SaveChanges* method on the context.

  * Redirect the user to the list of movies by returning a *RedirectToAction* method.

```
    [HttpPost]
    public ActionResult Save(Movie movie)
    {
        if (movie.Id == 0)
        {                
            _context.Movies.Add(movie);
        }
        else
        {
            var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
            movieInDb.Name = movie.Name;
            movieInDb.GenreId = movie.GenreId;
            movieInDb.NumberInStock = movie.NumberInStock;            
            movieInDb.ReleaseDate = movie.ReleaseDate;
        }

        _context.SaveChanges();

        return RedirectToAction("Index", "Movies");
    }
```  


&nbsp;
## 39 Add validation on customer save

* In *CustomersController.cs*, in the Save action, add *ModelState* validation. On save button click, redirect to remain in the same form if the model state is not valid.

```
    if (!ModelState.IsValid)
    {
        var viewModel = new CustomerFormViewModel
        {
            Customer = customer,
            MembershipTypes = _context.MembershipTypes.ToList()
        };

        return View("CustomerForm", viewModel);
    }
```

* Add validation message placeholders in the *CustomerForm* view.

```
<div class="form-group">
    @Html.LabelFor(m => m.Customer.Name)
    @Html.TextBoxFor(m => m.Customer.Name, new { @class = "form-control" })
    @Html.ValidationMessageFor(m => m.Customer.Name)
</div>
```
```
<div class="form-group">
    @Html.LabelFor(m => m.Customer.MembershipTypeId)
    @Html.DropDownListFor(m => m.Customer.MembershipTypeId,
         new SelectList(Model.MembershipTypes, "Id", "Name"),
          "Select Membership Type", new { @class = "form-control" })
    @Html.ValidationMessageFor(m => m.Customer.MembershipTypeId)
</div>
```


* In *Content/Site.css* style the *field-validation-error* class with red color.

```
.field-validation-error {
    color: red;
}
```

* Add a red border *for input-validation-error*.

```
.input-validation-error {
    border: 2px solid red;
}
```

* In the Customer model, override the message for the *Required* attribute in the data annotation.

```
[Required(ErrorMessage = "Please enter customer's name.")]
[StringLength(255)]
public string Name { get; set; }
```


&nbsp;
## 40 Custom validation with a business rule for customers age

* We will add a business rule, the requirement that only customers of 18+ years of age will be able to have a membership type different than "Pay As You Go".

* **Avoid magic numbers:** In the logic we will implement, we will need to check for the value of membership type Id. If it is not set, it is autoassigned to the zero value.  We have set the Id for "Pay As You Go" to 1 with the *PopulateMembershipTypes* migration. Define membership types inside the *MembershipType* model in order to avoid magic numbers in the code that will implement the valdation logic..

*Models/MembershipType.cs*
```
    public static readonly byte Unknown = 0;
    public static readonly byte PayAsYouGo = 1;
```

* **Validation logic:** In the models, add the class *Min18YearsForMembership* that derives from *ValidationAttribute*. Override the *isValid* method, select the overload that contains validation context, so that we can add the validation logic.

*Models/Min18YearsForMembership.cs*
```
using System.ComponentModel.DataAnnotations;
```
```
public class Min18YearsForMembership : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var customer = (Customer)validationContext.ObjectInstance;

        if (customer.MembershipTypeId == MembershipType.Unknown ||
            customer.MembershipTypeId == MembershipType.PayAsYouGo)
            return ValidationResult.Success;            

        if (customer.Birthdate == null)
            return new ValidationResult("Birthdate is required.");

        var age = GetAge((DateTime)customer.Birthdate);

        return (age >= 18)
            ? ValidationResult.Success
            : new ValidationResult("Customer should be at least 18 years old to go on a membership");
    }


    public static Int32 GetAge(DateTime dateOfBirth)
    // by ScArcher2 in https://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-in-c
    {
        var today = DateTime.Today;

        var a = (today.Year * 100 + today.Month) * 100 + today.Day;
        var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

        return (a - b) / 10000;
    }
}
```

* Apply the *Min18YearsForMembership* class as an attribute to the *Birthdate* property of the *Customer* model.

*Models/Customer.cs*
```
    [Display(Name="Date of Birth")]
    [Min18YearsForMembership]
    public DateTime? Birthdate { get; set; }
```


* Add the validation message placeholder in the *CustomerForm* view.

*Views/Customers/CustomerForm.cshtml*
```
<div class="form-group">
    @Html.LabelFor(m => m.Customer.Birthdate)
    @Html.TextBoxFor(m => m.Customer.Birthdate, "{0:d MMMM yyyy}", new { @class = "form-control" })
    @Html.ValidationMessageFor(m => m.Customer.Birthdate)
</div>
```



&nbsp;
## 41 Add validation summary messages

* Add the validation summary messages placeholder at the top of the form in the *CustomerForm* view.

```
@Html.ValidationSummary()
```

* In *Content/Site.css* style the *validation-summary-errors* class with red color.

```
.validation-summary-errors {
    color: red;
}
```

* Swap MembershipType and Birthdate input locations on the form to improve the user experience.



&nbsp;
## 42 Add client-side validation to the customer form

* Enable client-side validation for the CustomerForm. All standard data annotations will now be validated on the client. The custom business rules will still be validated on the server.

*Views/Customers/CustomerForm.cshtml*
```
@section scripts
{
    @Scripts.Render("~/bundles/jqueryval")
}
```


&nbsp;
## 43 Add an anti-forgery token to the customer form

* Add an *AntiForgeryToken* *Html* helper method call to the CustomerForm.

*Views/Customers/CustomerForm.cshtml*
```
    @Html.HiddenFor(m => m.Customer.Id)
    @Html.AntiForgeryToken()
    <button type="submit" class="btn btn-primary">Save</button>
```

* Then add the *ValidateAntiForgeryToken* attribute to the *Save* action in CustomersController.

*Controllers/CustomersController.cs*
```
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Save(Customer customer)
```


## 44 Validating the movies

* In *MoviesController.cs*, in the Save action, add *ModelState* validation. On save button click, remain in the MovieForm if the model state is not valid. Also add the anti-forgery token attribute.

*Controllers/MoviesController.cs*
```
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Save(Movie movie)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
        };

            return View("MovieForm", viewModel);
        }
  ...      
```

* Add validation message placeholders in the *MovieForm* view. Also call the *AntiForgeryToken* *Html* helper method and enable client-side validation.

*Views/Movies/MovieForm.cshtml*
```
@using (Html.BeginForm("Save", "Movies"))
{
    @Html.ValidationSummary()
    <div class="form-group">
        @Html.LabelFor(m => m.Movie.Name)
        @Html.TextBoxFor(m => m.Movie.Name, new { @class = "form-control" })
        @Html.ValidationMessageFor(m => m.Movie.Name)
    </div>
    <div class="form-group">
        @Html.LabelFor(m => m.Movie.DateAdded)
        @Html.TextBoxFor(m => m.Movie.DateAdded, "{0:d MMMM yyyy}", new { @class = "form-control" })
        @Html.ValidationMessageFor(m => m.Movie.DateAdded)
    </div>
    <div class="form-group">
        @Html.LabelFor(m => m.Movie.ReleaseDate)
        @Html.TextBoxFor(m => m.Movie.ReleaseDate, "{0:d MMMM yyyy}", new { @class = "form-control" })
        @Html.ValidationMessageFor(m => m.Movie.ReleaseDate)
    </div>
    <div class="form-group">
        @Html.LabelFor(m => m.Movie.GenreId)
        @Html.DropDownListFor(m => m.Movie.GenreId,
                     new SelectList(Model.Genres, "Id", "Name"), "", new { @class = "form-control" })
        @Html.ValidationMessageFor(m => m.Movie.GenreId)
    </div>
    <div class="form-group">
        @Html.LabelFor(m => m.Movie.NumberInStock)
        @Html.TextBoxFor(m => m.Movie.NumberInStock, new { @class = "form-control" })
        @Html.ValidationMessageFor(m => m.Movie.NumberInStock)
    </div>
    @Html.HiddenFor(m => m.Movie.Id)
    @Html.AntiForgeryToken()
    <button type="submit" class="btn btn-primary">Save</button>
}

@section scripts
{
    @Scripts.Render("~/bundles/jqueryval")
}
```

* The validation error styling has been already applied with customer validation.


* Add a range between 1 and 20 for the movies stock in the Movie model.

*Models/Movie.cs*
```
  [Display(Name = "Number in Stock")]
  [Range(1, 20)]
  public byte NumberInStock { get; set; }
```
