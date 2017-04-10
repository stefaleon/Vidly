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
* Cut the NavBar code from *_Layout.cshtml* and paste    it into *_Navbar.cshtml*.
* In place of the cut code in *_Layout.cshtml* add a call to the *Html.Partial* method.
```
@Html.Partial("_NavBar")
```
