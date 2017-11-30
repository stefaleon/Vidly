# Vidly
[The Complete ASP.NET MVC 5 Course](https://www.udemy.com/the-complete-aspnet-mvc-5-course/)

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
