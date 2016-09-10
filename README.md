<div>
<div>
<h2>Tutorial Overview</h2>
<p><font size="2"><span style="font-family:verdana,sans-serif">Xamarin.Android provides the ability to implement a RecyclerView helper class to rearrange a list of items.&nbsp; Additionally Xamarin.Android exposes a simple API to enable drag and drop between two View controls.&nbsp; Unfortunately, the RecyclerView helper class used to rearrange list items does not support dragging list items to controls outside of the RecyclerView control.</span></font></p>
<p><font size="2"><span style="font-family:verdana,sans-serif">To enable dragging list items from a RecyclerView to other views, we will create a layout file that uses a CardView control to gain access to long-press methods, and implement custom drag events.&nbsp; These drag events will support rearranging items within the RecyclerView, and also allow the user to drag list items to another view to delete them.<br>
</span></font></p>
<p><font size="2"><span style="font-family:verdana,sans-serif">For the purposes of this demonstration, we will create a simple contact management application that allows a user to work with a list of existing Contact records, re-arrange and delete them by dragging to the appropriate area on the screen, tap on each contact to view the contact details, and simulate adding new contacts to the list.<br>
</span></font></p>
<p><font size="2"><span style="font-family:verdana,sans-serif">This tutorial series covers the following activities:<br>
</span></font></p>
<ul><li><font size="2"><span style="font-family:verdana,sans-serif">Create a new Xamarin.Android project in Visual Studio</span></font></li>
<li><font size="2"><span style="font-family:verdana,sans-serif">Adding the necessary NuGet packages for RecyclerView and CardView controls<br>
</span></font></li>
<li><font size="2"><span style="font-family:verdana,sans-serif">Creating the Contact Model and View Model classes</span></font><br>
</li>
<li><font size="2"><span style="font-family:verdana,sans-serif">Defining the necessary layout definitions for the main, list item, and drag box style definitions<br>
</span></font></li>
<li><font size="2"><span style="font-family:verdana,sans-serif">Creating the View Holder class to set each list item's layout</span></font></li>
<li><font size="2"><span style="font-family:verdana,sans-serif">Creating an Adapter class and customizing it with Drag functionality for the CardView controls<br>
</span></font></li>
<li><font size="2"><span style="font-family:verdana,sans-serif">Creating the main Activity and customizing it to implement Drag and Drop functionality.<br>
</span></font></li></ul>
<p><span style="font-family:verdana,sans-serif"><font size="2">At the 
end of this tutorial you will have a fully functional, custom RecyclerView drag and drop implementation that extends the RecyclerView's drop range to any view control.&nbsp; With the knowledge you gain from this tutorial, you can go on to implement more custom drag and drop functionality in your own solution.<br>
</font></span></p>
<h2><a name="TOC-Source-Code"></a>Source Code</h2>
<p><span style="font-family:verdana,sans-serif">On GitHub: <a href="https://github.com/C0D3Name/DragDropDroid" target="_blank">https://github.com/C0D3Name/DragDropDroid</a><br>
</span></p>
<span style="font-family:verdana,sans-serif">The
 source code for this tutorial series can be found on my GitHub page.<br>
</span>
<hr>
<h2>Create the new Restaurant Billing solution</h2>
<h3><a name="TOC-Overview"></a><span style="font-family:verdana,sans-serif">Overview<br>
</span></h3>
<p><span style="font-family:verdana,sans-serif">In this section, we will
 create our new solution that will contain one project: our Xamarin.Android project.</span></p>
<h3><a name="TOC-Steps"></a><span style="font-family:verdana,sans-serif">Steps</span><br>
</h3>


<div><font face="verdana,sans-serif">Open a new instance of Visual Studio and select File &gt; New Project.</font><font face="verdana,sans-serif">&nbsp; Search for 'xamarin android' and select the 'Blank App (Android) option (be sure to select the C# project type).<br>
<div style="display:block;text-align:left"><a href="https://sites.google.com/site/netdeveloperblog/xamarin/android/customdragdrop/01_NewSolution.PNG?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/android/customdragdrop/01_NewSolution.PNG" style="width:100%" border="0"></a></div>
<br>
</font></div>
<font face="verdana,sans-serif">Name the solution 'DragDropDroid', and click OK.</font><font face="verdana,sans-serif">&nbsp; At this point, you should have a solution called 'DragDropDroid' with an Android project, also named 'DragDropDroid'.<br>
<br>
</font></div>
<font face="verdana,sans-serif">Go ahead and delete the MainActivity.cs and GettingStarted.Xamarin files, we won't need them for the tutorial.&nbsp; <br>
<br>
Once finished, your project should look similar to this in the solution explorer:<br>
<div style="display:block;text-align:left"><a href="https://sites.google.com/site/netdeveloperblog/xamarin/android/customdragdrop/02_Solution.PNG?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/android/customdragdrop/02_Solution.PNG" border="0"></a></div>
</font><br>
<div>

<hr>
<h2><a name="TOC-Create-the-RestaurantBilling.Core-PCL"></a>Add RecyclerView and CardView packages with NuGet<br>
</h2>
<h3><a name="TOC-Overview1"></a><span style="font-family:verdana,sans-serif">Overview</span></h3>
<p><span style="font-family:verdana,sans-serif">To gain access to the RecyclerView and CardView controls in Xamarin.Android, we will need to add two NuGet packages to our project</span></p>
<ul><li><span style="font-family:verdana,sans-serif">Xamarin.Android.Support.v7.RecyclerView</span></li>
<li><span style="font-family:verdana,sans-serif">Xamarin.Android.Support.v7.CardView</span></li></ul>
<p><span style="font-family:verdana,sans-serif">**Note: You will also need to have the Android SDK support libraries installed or you may run into build/runtime errors.&nbsp; Ensure you have these libaries installed by downloading them using your Android SDK Manager.</span><br>
</p>
<h3><a name="TOC-Steps1"></a><span style="font-family:verdana,sans-serif">Steps</span><br>
</h3>
<p><font size="2"><span style="font-family:verdana,sans-serif">From the Visual Studio menu, select Tools &gt; NuGet Package Manger &gt; Manage Packages for Solution. <br>
</span></font></p>
<p><font size="2"><span style="font-family:verdana,sans-serif">Select 
the 'Browse' tab, and search for Xamarin.Android.Support.v7.RecyclerView.&nbsp; Check the checkbox for DragDropDroid and click the Install button.&nbsp; <br>
</span></font></p>
<font size="2"><span style="font-family:verdana,sans-serif">This will install the necessary packages to utilize RecyclerView in the project.<br>
<div style="display:block;text-align:left"><a href="https://sites.google.com/site/netdeveloperblog/xamarin/android/customdragdrop/03_NuGet1.PNG?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/android/customdragdrop/03_NuGet1.PNG" style="width:100%" border="0"></a></div>
<br>
</span></font></div>
<div><font size="2"><span style="font-family:verdana,sans-serif">Now search for Xamarin.Android.Support.v7.CardView.&nbsp; Check the checkbox for DragDropDroid and click the Install button.<br>
<br>
</span></font></div>
<div><font size="2"><span style="font-family:verdana,sans-serif">This will install the necessary packages to utilize CardView in the project.<br>
<div style="display:block;text-align:left"><a href="https://sites.google.com/site/netdeveloperblog/xamarin/android/customdragdrop/04_NuGet2.PNG?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/android/customdragdrop/04_NuGet2.PNG" style="width:100%" border="0"></a></div>
<br>
</span></font></div>
<div><font size="2"><span style="font-family:verdana,sans-serif">Go ahead and build the solution to make sure everything installed correctly.<br>
</span></font>
<hr>
<h2>Implement the Contact Model and MainViewModel classes</h2>
<h3><a name="TOC-Overview2"></a><span style="font-family:verdana,sans-serif">Overview</span></h3>
<p><span style="font-family:verdana,sans-serif">Although the application will be just a single Activity, we'll still follow the MVVM approach by creating our data model (Contact) and view model classes.&nbsp; Our Contact model class will store each contact's name and email address.&nbsp; MainViewModel will simply store a list of contacts, and initialize it with some sample data.<b><br>
</b></span></p>
<h3><a name="TOC-Steps2"></a><span style="font-family:verdana,sans-serif">Steps</span></h3>
<p><font size="2"><span style="font-family:verdana,sans-serif">Create a new folder in the DragDropDroid project called Models.&nbsp; Add a new class called 'Contact' with the following code:</span></font><br>
</p>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">namespace</span> DragDropDroid.Models {
    <span style="color:rgb(153,153,136);font-style:italic">// Super simple User Contact model containin a contact name and email.</span>
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">class</span> Contact {
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(0,134,179)">string</span> Name { get; <span style="color:rgb(0,134,179)">set</span>; }
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(0,134,179)">string</span> Email { get; <span style="color:rgb(0,134,179)">set</span>; }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPm5hbWVzcGFjZSBEcmFnRHJvcERyb2lkLk1vZGVscyB7PGJyPsKgwqDCoCAvLyBT dXBlciBzaW1wbGUgVXNlciBDb250YWN0IG1vZGVsIGNvbnRhaW5pbiBhIGNvbnRhY3QgbmFtZSBh bmQgZW1haWwuPGJyPsKgwqDCoCBwdWJsaWMgY2xhc3MgQ29udGFjdCB7PGJyPsKgwqDCoMKgwqDC oMKgIHB1YmxpYyBzdHJpbmcgTmFtZSB7IGdldDsgc2V0OyB9PGJyPsKgwqDCoMKgwqDCoMKgIHB1 YmxpYyBzdHJpbmcgRW1haWwgeyBnZXQ7IHNldDsgfTxicj7CoMKgwqAgfTxicj59PGJyPgpgYGA=">​</div>
</div>
<p><font size="2"><span style="font-family:verdana,sans-serif">Create a new folder in the DragDropProject called 'ViewModels'.&nbsp; Add a new class called 'MainViewModel' with the following code.</span></font><br>
</p>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> System.Collections.Generic;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> DragDropDroid.Models;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> DragDropDroid.ViewModels {
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">class</span> MainViewModel {
        <span style="color:rgb(153,153,136);font-style:italic">// List of contacts Adapter will bind to.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> List&lt;Contact&gt; Contacts = <span style="color:rgb(51,51,51);font-weight:bold">new</span> List&lt;Contact&gt;();

        <span style="color:rgb(51,51,51);font-weight:bold">public</span> MainViewModel() {
            CreateSampleData();
        }

        <span style="color:rgb(153,153,136);font-style:italic">// Create some sample data to mess around with.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">private</span> <span style="color:rgb(51,51,51);font-weight:bold">void</span> CreateSampleData() {
            <span style="color:rgb(51,51,51);font-weight:bold">if</span>(Contacts.Count &lt; <span style="color:rgb(0,128,128)">1</span>) {
                <span style="color:rgb(51,51,51);font-weight:bold">for</span>( <span style="color:rgb(51,51,51);font-weight:bold">int</span> ii = <span style="color:rgb(0,128,128)">1</span>; ii &lt; <span style="color:rgb(0,128,128)">10</span>; ii++) {
                    Contacts.Add(
                        <span style="color:rgb(51,51,51);font-weight:bold">new</span> Contact() {
                            Name = $<span style="color:rgb(221,17,68)">"Person {ii}"</span>,
                            Email = $<span style="color:rgb(221,17,68)">"Person{ii}@fakeaddy.com"</span>
                        });
                }
            }
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnVzaW5nIFN5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljOzxicj51c2luZyBEcmFn RHJvcERyb2lkLk1vZGVsczs8YnI+PGJyPm5hbWVzcGFjZSBEcmFnRHJvcERyb2lkLlZpZXdNb2Rl bHMgezxicj7CoMKgwqAgcHVibGljIGNsYXNzIE1haW5WaWV3TW9kZWwgezxicj7CoMKgwqDCoMKg wqDCoCAvLyBMaXN0IG9mIGNvbnRhY3RzIEFkYXB0ZXIgd2lsbCBiaW5kIHRvLjxicj7CoMKgwqDC oMKgwqDCoCBwdWJsaWMgTGlzdCZsdDtDb250YWN0Jmd0OyBDb250YWN0cyA9IG5ldyBMaXN0Jmx0 O0NvbnRhY3QmZ3Q7KCk7PGJyPjxicj7CoMKgwqDCoMKgwqDCoCBwdWJsaWMgTWFpblZpZXdNb2Rl bCgpIHs8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBDcmVhdGVTYW1wbGVEYXRhKCk7PGJyPsKg wqDCoMKgwqDCoMKgIH08YnI+PGJyPsKgwqDCoMKgwqDCoMKgIC8vIENyZWF0ZSBzb21lIHNhbXBs ZSBkYXRhIHRvIG1lc3MgYXJvdW5kIHdpdGguPGJyPsKgwqDCoMKgwqDCoMKgIHByaXZhdGUgdm9p ZCBDcmVhdGVTYW1wbGVEYXRhKCkgezxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIGlmKENvbnRh Y3RzLkNvdW50ICZsdDsgMSkgezxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqAgZm9y KCBpbnQgaWkgPSAxOyBpaSAmbHQ7IDEwOyBpaSsrKSB7PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgIENvbnRhY3RzLkFkZCg8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBuZXcgQ29udGFjdCgpIHs8YnI+wqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgIE5hbWUgPSAkIlBlcnNv biB7aWl9Iiw8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqDCoMKgIEVtYWlsID0gJCJQZXJzb257aWl9QGZha2VhZGR5LmNvbSI8YnI+wqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCB9KTs8YnI+wqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgIH08YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoCB9PGJyPsKgwqDC oMKgwqDCoMKgIH08YnI+wqDCoMKgIH08YnI+fTxicj4KYGBg">​</div>
</div>
<hr>
<h2>Defining the necessary layout files<br>
</h2>
<h3><a name="TOC-Overview2"></a><span style="font-family:verdana,sans-serif">Overview</span></h3>
<p><span style="font-family:verdana,sans-serif">We will need to update the Main layout file and add the following controls</span></p>
<ul><li><span style="font-family:verdana,sans-serif">A box where Contacts can be dragged to be deleted</span></li>
<li><span style="font-family:verdana,sans-serif">A button for adding new contacts</span></li>
<li><span style="font-family:verdana,sans-serif">A recycler view that will display our list of contacts.</span></li></ul>
<p><span style="font-family:verdana,sans-serif">We will then need to define another layout file to describe the layout of each Contact list item that is displayed in our recycler view.&nbsp; This layout file will simply have the following components</span></p>
<ul><li><span style="font-family:verdana,sans-serif">A CardView to give us access to long-click methods<br>
</span></li>
<li><span style="font-family:verdana,sans-serif">A TextView for the contact's name, <br>
</span></li>
<li><span style="font-family:verdana,sans-serif">Another TextView for the contact's email.</span></li></ul>
<p><span style="font-family:verdana,sans-serif">We will also define the shape and style our delete box will have by creating two xml files, one for the default style, and one for the style that will be applied when we are dragging contacts over the box.</span></p>
<h3><a name="TOC-Steps2"></a><span style="font-family:verdana,sans-serif">Steps</span></h3>
<font size="2"><span style="font-family:verdana,sans-serif">Open the Resources/layout/Main.axml file, switch to Source view, and add the following code:</span></font><br>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(153,153,153);font-weight:bold">&lt;?xml version="1.0" encoding="utf-8"?&gt;</span>
<span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">LinearLayout</span> <span style="color:rgb(0,128,128)">xmlns:android</span>=<span style="color:rgb(221,17,68)">"http://schemas.android.com/apk/res/android"</span>
    <span style="color:rgb(0,128,128)">android:orientation</span>=<span style="color:rgb(221,17,68)">"vertical"</span>
    <span style="color:rgb(0,128,128)">android:layout_width</span>=<span style="color:rgb(221,17,68)">"match_parent"</span>
    <span style="color:rgb(0,128,128)">android:layout_height</span>=<span style="color:rgb(221,17,68)">"match_parent"</span>&gt;</span>
  <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">LinearLayout</span>
      <span style="color:rgb(0,128,128)">android:id</span>=<span style="color:rgb(221,17,68)">"@+id/VCDropArea"</span>
      <span style="color:rgb(0,128,128)">android:layout_width</span>=<span style="color:rgb(221,17,68)">"match_parent"</span>
      <span style="color:rgb(0,128,128)">android:layout_height</span>=<span style="color:rgb(221,17,68)">"100dp"</span>
      <span style="color:rgb(0,128,128)">android:background</span>=<span style="color:rgb(221,17,68)">"@drawable/shape"</span>&gt;</span>
    <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">TextView</span>
      <span style="color:rgb(0,128,128)">android:text</span>=<span style="color:rgb(221,17,68)">"Drag here to delete"</span>
      <span style="color:rgb(0,128,128)">android:layout_width</span>=<span style="color:rgb(221,17,68)">"match_parent"</span>
      <span style="color:rgb(0,128,128)">android:layout_height</span>=<span style="color:rgb(221,17,68)">"match_parent"</span>
      <span style="color:rgb(0,128,128)">android:gravity</span>=<span style="color:rgb(221,17,68)">"center"</span>/&gt;</span>
  <span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">LinearLayout</span>&gt;</span>
  <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">Button</span>
    <span style="color:rgb(0,128,128)">android:id</span>=<span style="color:rgb(221,17,68)">"@+id/VCAddContact"</span>
    <span style="color:rgb(0,128,128)">android:text</span>=<span style="color:rgb(221,17,68)">"Add Contact"</span>
    <span style="color:rgb(0,128,128)">android:layout_width</span>=<span style="color:rgb(221,17,68)">"match_parent"</span>
    <span style="color:rgb(0,128,128)">android:layout_height</span>=<span style="color:rgb(221,17,68)">"wrap_content"</span>
    <span style="color:rgb(0,128,128)">android:layout_marginTop</span>=<span style="color:rgb(221,17,68)">"10dp"</span>
    <span style="color:rgb(0,128,128)">android:layout_marginBottom</span>=<span style="color:rgb(221,17,68)">"10dp"</span>/&gt;</span>
  <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">android.support.v7.widget.RecyclerView</span>
      <span style="color:rgb(0,128,128)">android:id</span>=<span style="color:rgb(221,17,68)">"@+id/VCContacts"</span>
      <span style="color:rgb(0,128,128)">android:background</span>=<span style="color:rgb(221,17,68)">"#3b3b3a"</span>
      <span style="color:rgb(0,128,128)">android:layout_width</span>=<span style="color:rgb(221,17,68)">"match_parent"</span>
      <span style="color:rgb(0,128,128)">android:layout_height</span>=<span style="color:rgb(221,17,68)">"wrap_content"</span> /&gt;</span>
<span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">LinearLayout</span>&gt;</span>
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgWE1MPGJyPiZsdDs/eG1sIHZlcnNpb249IjEuMCIgZW5jb2Rpbmc9InV0Zi04Ij8mZ3Q7PGJy PiZsdDtMaW5lYXJMYXlvdXQgeG1sbnM6YW5kcm9pZD0iaHR0cDovL3NjaGVtYXMuYW5kcm9pZC5j b20vYXBrL3Jlcy9hbmRyb2lkIjxicj7CoMKgwqAgYW5kcm9pZDpvcmllbnRhdGlvbj0idmVydGlj YWwiPGJyPsKgwqDCoCBhbmRyb2lkOmxheW91dF93aWR0aD0ibWF0Y2hfcGFyZW50Ijxicj7CoMKg wqAgYW5kcm9pZDpsYXlvdXRfaGVpZ2h0PSJtYXRjaF9wYXJlbnQiJmd0Ozxicj7CoCAmbHQ7TGlu ZWFyTGF5b3V0PGJyPsKgwqDCoMKgwqAgYW5kcm9pZDppZD0iQCtpZC9WQ0Ryb3BBcmVhIjxicj7C oMKgwqDCoMKgIGFuZHJvaWQ6bGF5b3V0X3dpZHRoPSJtYXRjaF9wYXJlbnQiPGJyPsKgwqDCoMKg wqAgYW5kcm9pZDpsYXlvdXRfaGVpZ2h0PSIxMDBkcCI8YnI+wqDCoMKgwqDCoCBhbmRyb2lkOmJh Y2tncm91bmQ9IkBkcmF3YWJsZS9zaGFwZSImZ3Q7PGJyPsKgwqDCoCAmbHQ7VGV4dFZpZXc8YnI+ wqDCoMKgwqDCoCBhbmRyb2lkOnRleHQ9IkRyYWcgaGVyZSB0byBkZWxldGUiPGJyPsKgwqDCoMKg wqAgYW5kcm9pZDpsYXlvdXRfd2lkdGg9Im1hdGNoX3BhcmVudCI8YnI+wqDCoMKgwqDCoCBhbmRy b2lkOmxheW91dF9oZWlnaHQ9Im1hdGNoX3BhcmVudCI8YnI+wqDCoMKgwqDCoCBhbmRyb2lkOmdy YXZpdHk9ImNlbnRlciIvJmd0Ozxicj7CoCAmbHQ7L0xpbmVhckxheW91dCZndDs8YnI+wqAgJmx0 O0J1dHRvbjxicj7CoMKgwqAgYW5kcm9pZDppZD0iQCtpZC9WQ0FkZENvbnRhY3QiPGJyPsKgwqDC oCBhbmRyb2lkOnRleHQ9IkFkZCBDb250YWN0Ijxicj7CoMKgwqAgYW5kcm9pZDpsYXlvdXRfd2lk dGg9Im1hdGNoX3BhcmVudCI8YnI+wqDCoMKgIGFuZHJvaWQ6bGF5b3V0X2hlaWdodD0id3JhcF9j b250ZW50Ijxicj7CoMKgwqAgYW5kcm9pZDpsYXlvdXRfbWFyZ2luVG9wPSIxMGRwIjxicj7CoMKg wqAgYW5kcm9pZDpsYXlvdXRfbWFyZ2luQm90dG9tPSIxMGRwIi8mZ3Q7PGJyPsKgICZsdDthbmRy b2lkLnN1cHBvcnQudjcud2lkZ2V0LlJlY3ljbGVyVmlldzxicj7CoMKgwqDCoMKgIGFuZHJvaWQ6 aWQ9IkAraWQvVkNDb250YWN0cyI8YnI+wqDCoMKgwqDCoCBhbmRyb2lkOmJhY2tncm91bmQ9IiMz YjNiM2EiPGJyPsKgwqDCoMKgwqAgYW5kcm9pZDpsYXlvdXRfd2lkdGg9Im1hdGNoX3BhcmVudCI8 YnI+wqDCoMKgwqDCoCBhbmRyb2lkOmxheW91dF9oZWlnaHQ9IndyYXBfY29udGVudCIgLyZndDs8 YnI+Jmx0Oy9MaW5lYXJMYXlvdXQmZ3Q7PGJyPgpgYGA=">​</div>
</div>
<p><font size="2"><span style="font-family:verdana,sans-serif">Create a new Android Layout file in Resources/layout called 'ContactListItem.axml' and add the following code:</span></font><br>
</p>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(153,153,153);font-weight:bold">&lt;?xml version="1.0" encoding="utf-8"?&gt;</span>
<span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">android.support.v7.widget.CardView</span> <span style="color:rgb(0,128,128)">xmlns:android</span>=<span style="color:rgb(221,17,68)">"http://schemas.android.com/apk/res/android"</span>
                                    <span style="color:rgb(0,128,128)">android:id</span>=<span style="color:rgb(221,17,68)">"@+id/VCContactCardView"</span>
    <span style="color:rgb(0,128,128)">android:layout_width</span>=<span style="color:rgb(221,17,68)">"match_parent"</span>
    <span style="color:rgb(0,128,128)">android:layout_height</span>=<span style="color:rgb(221,17,68)">"wrap_content"</span>
    <span style="color:rgb(0,128,128)">android:layout_marginTop</span>=<span style="color:rgb(221,17,68)">"8dp"</span>
    <span style="color:rgb(0,128,128)">android:layout_marginLeft</span>=<span style="color:rgb(221,17,68)">"8dp"</span>
    <span style="color:rgb(0,128,128)">android:layout_marginRight</span>=<span style="color:rgb(221,17,68)">"8dp"</span>&gt;</span>
  <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">LinearLayout</span>
        <span style="color:rgb(0,128,128)">android:layout_width</span>=<span style="color:rgb(221,17,68)">"match_parent"</span>
        <span style="color:rgb(0,128,128)">android:layout_height</span>=<span style="color:rgb(221,17,68)">"wrap_content"</span>
        <span style="color:rgb(0,128,128)">android:orientation</span>=<span style="color:rgb(221,17,68)">"vertical"</span>&gt;</span>
    <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">TextView</span>
          <span style="color:rgb(0,128,128)">android:id</span>=<span style="color:rgb(221,17,68)">"@+id/VCContactName"</span>
          <span style="color:rgb(0,128,128)">android:layout_alignParentBottom</span>=<span style="color:rgb(221,17,68)">"true"</span>
          <span style="color:rgb(0,128,128)">android:textAppearance</span>=<span style="color:rgb(221,17,68)">"?android:attr/textAppearanceLarge"</span>
          <span style="color:rgb(0,128,128)">android:layout_marginLeft</span>=<span style="color:rgb(221,17,68)">"16dp"</span>
          <span style="color:rgb(0,128,128)">android:layout_marginRight</span>=<span style="color:rgb(221,17,68)">"16dp"</span>
          <span style="color:rgb(0,128,128)">android:layout_width</span>=<span style="color:rgb(221,17,68)">"match_parent"</span>
          <span style="color:rgb(0,128,128)">android:layout_height</span>=<span style="color:rgb(221,17,68)">"wrap_content"</span> /&gt;</span>
    <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">TextView</span>
          <span style="color:rgb(0,128,128)">android:id</span>=<span style="color:rgb(221,17,68)">"@+id/VCContactEmail"</span>
          <span style="color:rgb(0,128,128)">android:layout_alignParentBottom</span>=<span style="color:rgb(221,17,68)">"true"</span>
          <span style="color:rgb(0,128,128)">android:textAppearance</span>=<span style="color:rgb(221,17,68)">"?android:attr/textAppearanceMedium"</span>
          <span style="color:rgb(0,128,128)">android:layout_marginLeft</span>=<span style="color:rgb(221,17,68)">"16dp"</span>
          <span style="color:rgb(0,128,128)">android:layout_marginRight</span>=<span style="color:rgb(221,17,68)">"16dp"</span>
          <span style="color:rgb(0,128,128)">android:layout_width</span>=<span style="color:rgb(221,17,68)">"match_parent"</span>
          <span style="color:rgb(0,128,128)">android:layout_height</span>=<span style="color:rgb(221,17,68)">"wrap_content"</span> /&gt;</span>
  <span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">LinearLayout</span>&gt;</span>
<span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">android.support.v7.widget.CardView</span>&gt;</span>
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgWE1MPGJyPiZsdDs/eG1sIHZlcnNpb249IjEuMCIgZW5jb2Rpbmc9InV0Zi04Ij8mZ3Q7PGJy PiZsdDthbmRyb2lkLnN1cHBvcnQudjcud2lkZ2V0LkNhcmRWaWV3IHhtbG5zOmFuZHJvaWQ9Imh0 dHA6Ly9zY2hlbWFzLmFuZHJvaWQuY29tL2Fway9yZXMvYW5kcm9pZCI8YnI+wqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oCBhbmRyb2lkOmlkPSJAK2lkL1ZDQ29udGFjdENhcmRWaWV3Ijxicj7CoMKgwqAgYW5kcm9pZDps YXlvdXRfd2lkdGg9Im1hdGNoX3BhcmVudCI8YnI+wqDCoMKgIGFuZHJvaWQ6bGF5b3V0X2hlaWdo dD0id3JhcF9jb250ZW50Ijxicj7CoMKgwqAgYW5kcm9pZDpsYXlvdXRfbWFyZ2luVG9wPSI4ZHAi PGJyPsKgwqDCoCBhbmRyb2lkOmxheW91dF9tYXJnaW5MZWZ0PSI4ZHAiPGJyPsKgwqDCoCBhbmRy b2lkOmxheW91dF9tYXJnaW5SaWdodD0iOGRwIiZndDs8YnI+wqAgJmx0O0xpbmVhckxheW91dDxi cj7CoMKgwqDCoMKgwqDCoCBhbmRyb2lkOmxheW91dF93aWR0aD0ibWF0Y2hfcGFyZW50Ijxicj7C oMKgwqDCoMKgwqDCoCBhbmRyb2lkOmxheW91dF9oZWlnaHQ9IndyYXBfY29udGVudCI8YnI+wqDC oMKgwqDCoMKgwqAgYW5kcm9pZDpvcmllbnRhdGlvbj0idmVydGljYWwiJmd0Ozxicj7CoMKgwqAg Jmx0O1RleHRWaWV3PGJyPsKgwqDCoMKgwqDCoMKgwqDCoCBhbmRyb2lkOmlkPSJAK2lkL1ZDQ29u dGFjdE5hbWUiPGJyPsKgwqDCoMKgwqDCoMKgwqDCoCBhbmRyb2lkOmxheW91dF9hbGlnblBhcmVu dEJvdHRvbT0idHJ1ZSI8YnI+wqDCoMKgwqDCoMKgwqDCoMKgIGFuZHJvaWQ6dGV4dEFwcGVhcmFu Y2U9Ij9hbmRyb2lkOmF0dHIvdGV4dEFwcGVhcmFuY2VMYXJnZSI8YnI+wqDCoMKgwqDCoMKgwqDC oMKgIGFuZHJvaWQ6bGF5b3V0X21hcmdpbkxlZnQ9IjE2ZHAiPGJyPsKgwqDCoMKgwqDCoMKgwqDC oCBhbmRyb2lkOmxheW91dF9tYXJnaW5SaWdodD0iMTZkcCI8YnI+wqDCoMKgwqDCoMKgwqDCoMKg IGFuZHJvaWQ6bGF5b3V0X3dpZHRoPSJtYXRjaF9wYXJlbnQiPGJyPsKgwqDCoMKgwqDCoMKgwqDC oCBhbmRyb2lkOmxheW91dF9oZWlnaHQ9IndyYXBfY29udGVudCIgLyZndDs8YnI+wqDCoMKgICZs dDtUZXh0Vmlldzxicj7CoMKgwqDCoMKgwqDCoMKgwqAgYW5kcm9pZDppZD0iQCtpZC9WQ0NvbnRh Y3RFbWFpbCI8YnI+wqDCoMKgwqDCoMKgwqDCoMKgIGFuZHJvaWQ6bGF5b3V0X2FsaWduUGFyZW50 Qm90dG9tPSJ0cnVlIjxicj7CoMKgwqDCoMKgwqDCoMKgwqAgYW5kcm9pZDp0ZXh0QXBwZWFyYW5j ZT0iP2FuZHJvaWQ6YXR0ci90ZXh0QXBwZWFyYW5jZU1lZGl1bSI8YnI+wqDCoMKgwqDCoMKgwqDC oMKgIGFuZHJvaWQ6bGF5b3V0X21hcmdpbkxlZnQ9IjE2ZHAiPGJyPsKgwqDCoMKgwqDCoMKgwqDC oCBhbmRyb2lkOmxheW91dF9tYXJnaW5SaWdodD0iMTZkcCI8YnI+wqDCoMKgwqDCoMKgwqDCoMKg IGFuZHJvaWQ6bGF5b3V0X3dpZHRoPSJtYXRjaF9wYXJlbnQiPGJyPsKgwqDCoMKgwqDCoMKgwqDC oCBhbmRyb2lkOmxheW91dF9oZWlnaHQ9IndyYXBfY29udGVudCIgLyZndDs8YnI+wqAgJmx0Oy9M aW5lYXJMYXlvdXQmZ3Q7PGJyPiZsdDsvYW5kcm9pZC5zdXBwb3J0LnY3LndpZGdldC5DYXJkVmll dyZndDs8YnI+CmBgYA==">​</div>
</div>
<p><font size="2"><span style="font-family:verdana,sans-serif">Now create an XML file in Resources/drawable called 'shape.xml' and add the following code:</span></font><br>
</p>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(153,153,153);font-weight:bold">&lt;?xml version="1.0" encoding="UTF-8"?&gt;</span>
<span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">shape</span> <span style="color:rgb(0,128,128)">xmlns:android</span>=<span style="color:rgb(221,17,68)">"http://schemas.android.com/apk/res/android"</span>
    <span style="color:rgb(0,128,128)">android:shape</span>=<span style="color:rgb(221,17,68)">"rectangle"</span> &gt;</span>

  <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">stroke</span>
      <span style="color:rgb(0,128,128)">android:width</span>=<span style="color:rgb(221,17,68)">"2dp"</span>
      <span style="color:rgb(0,128,128)">android:color</span>=<span style="color:rgb(221,17,68)">"#FFFFFFFF"</span> /&gt;</span>

  <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">gradient</span>
      <span style="color:rgb(0,128,128)">android:angle</span>=<span style="color:rgb(221,17,68)">"225"</span>
      <span style="color:rgb(0,128,128)">android:endColor</span>=<span style="color:rgb(221,17,68)">"#3d4752"</span>
      <span style="color:rgb(0,128,128)">android:startColor</span>=<span style="color:rgb(221,17,68)">"#424243"</span> /&gt;</span>

  <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">corners</span>
      <span style="color:rgb(0,128,128)">android:bottomLeftRadius</span>=<span style="color:rgb(221,17,68)">"7dp"</span>
      <span style="color:rgb(0,128,128)">android:bottomRightRadius</span>=<span style="color:rgb(221,17,68)">"7dp"</span>
      <span style="color:rgb(0,128,128)">android:topLeftRadius</span>=<span style="color:rgb(221,17,68)">"7dp"</span>
      <span style="color:rgb(0,128,128)">android:topRightRadius</span>=<span style="color:rgb(221,17,68)">"7dp"</span> /&gt;</span>

<span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">shape</span>&gt;</span>
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgWE1MPGJyPiZsdDs/eG1sIHZlcnNpb249IjEuMCIgZW5jb2Rpbmc9IlVURi04Ij8mZ3Q7PGJy PiZsdDtzaGFwZSB4bWxuczphbmRyb2lkPSJodHRwOi8vc2NoZW1hcy5hbmRyb2lkLmNvbS9hcGsv cmVzL2FuZHJvaWQiPGJyPsKgwqDCoCBhbmRyb2lkOnNoYXBlPSJyZWN0YW5nbGUiICZndDs8YnI+ PGJyPsKgICZsdDtzdHJva2U8YnI+wqDCoMKgwqDCoCBhbmRyb2lkOndpZHRoPSIyZHAiPGJyPsKg wqDCoMKgwqAgYW5kcm9pZDpjb2xvcj0iI0ZGRkZGRkZGIiAvJmd0Ozxicj48YnI+wqAgJmx0O2dy YWRpZW50PGJyPsKgwqDCoMKgwqAgYW5kcm9pZDphbmdsZT0iMjI1Ijxicj7CoMKgwqDCoMKgIGFu ZHJvaWQ6ZW5kQ29sb3I9IiMzZDQ3NTIiPGJyPsKgwqDCoMKgwqAgYW5kcm9pZDpzdGFydENvbG9y PSIjNDI0MjQzIiAvJmd0Ozxicj48YnI+wqAgJmx0O2Nvcm5lcnM8YnI+wqDCoMKgwqDCoCBhbmRy b2lkOmJvdHRvbUxlZnRSYWRpdXM9IjdkcCI8YnI+wqDCoMKgwqDCoCBhbmRyb2lkOmJvdHRvbVJp Z2h0UmFkaXVzPSI3ZHAiPGJyPsKgwqDCoMKgwqAgYW5kcm9pZDp0b3BMZWZ0UmFkaXVzPSI3ZHAi PGJyPsKgwqDCoMKgwqAgYW5kcm9pZDp0b3BSaWdodFJhZGl1cz0iN2RwIiAvJmd0Ozxicj48YnI+ Jmx0Oy9zaGFwZSZndDs8YnI+CmBgYA==">​</div>
</div>
<p><font size="2"><span style="font-family:verdana,sans-serif">Finally, create another XML file in Resources/drawable called 'shape_droptarget.xml' and add the following code:</span></font></p>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(153,153,153);font-weight:bold">&lt;?xml version="1.0" encoding="UTF-8"?&gt;</span>
<span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">shape</span> <span style="color:rgb(0,128,128)">xmlns:android</span>=<span style="color:rgb(221,17,68)">"http://schemas.android.com/apk/res/android"</span>
    <span style="color:rgb(0,128,128)">android:shape</span>=<span style="color:rgb(221,17,68)">"rectangle"</span> &gt;</span>

  <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">stroke</span>
      <span style="color:rgb(0,128,128)">android:width</span>=<span style="color:rgb(221,17,68)">"2dp"</span>
      <span style="color:rgb(0,128,128)">android:color</span>=<span style="color:rgb(221,17,68)">"#3776b9"</span> /&gt;</span>

  <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">gradient</span>
      <span style="color:rgb(0,128,128)">android:angle</span>=<span style="color:rgb(221,17,68)">"225"</span>
      <span style="color:rgb(0,128,128)">android:endColor</span>=<span style="color:rgb(221,17,68)">"#3d4752"</span>
      <span style="color:rgb(0,128,128)">android:startColor</span>=<span style="color:rgb(221,17,68)">"#424243"</span> /&gt;</span>

  <span style="color:rgb(0,0,128);font-weight:normal">&lt;<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">corners</span>
      <span style="color:rgb(0,128,128)">android:bottomLeftRadius</span>=<span style="color:rgb(221,17,68)">"7dp"</span>
      <span style="color:rgb(0,128,128)">android:bottomRightRadius</span>=<span style="color:rgb(221,17,68)">"7dp"</span>
      <span style="color:rgb(0,128,128)">android:topLeftRadius</span>=<span style="color:rgb(221,17,68)">"7dp"</span>
      <span style="color:rgb(0,128,128)">android:topRightRadius</span>=<span style="color:rgb(221,17,68)">"7dp"</span> /&gt;</span>

<span style="color:rgb(0,0,128);font-weight:normal">&lt;/<span style="color:rgb(153,0,0);font-weight:bold;color:rgb(0,0,128);font-weight:normal">shape</span>&gt;</span>
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgWE1MPGJyPiZsdDs/eG1sIHZlcnNpb249IjEuMCIgZW5jb2Rpbmc9IlVURi04Ij8mZ3Q7PGJy PiZsdDtzaGFwZSB4bWxuczphbmRyb2lkPSJodHRwOi8vc2NoZW1hcy5hbmRyb2lkLmNvbS9hcGsv cmVzL2FuZHJvaWQiPGJyPsKgwqDCoCBhbmRyb2lkOnNoYXBlPSJyZWN0YW5nbGUiICZndDs8YnI+ PGJyPsKgICZsdDtzdHJva2U8YnI+wqDCoMKgwqDCoCBhbmRyb2lkOndpZHRoPSIyZHAiPGJyPsKg wqDCoMKgwqAgYW5kcm9pZDpjb2xvcj0iIzM3NzZiOSIgLyZndDs8YnI+PGJyPsKgICZsdDtncmFk aWVudDxicj7CoMKgwqDCoMKgIGFuZHJvaWQ6YW5nbGU9IjIyNSI8YnI+wqDCoMKgwqDCoCBhbmRy b2lkOmVuZENvbG9yPSIjM2Q0NzUyIjxicj7CoMKgwqDCoMKgIGFuZHJvaWQ6c3RhcnRDb2xvcj0i IzQyNDI0MyIgLyZndDs8YnI+PGJyPsKgICZsdDtjb3JuZXJzPGJyPsKgwqDCoMKgwqAgYW5kcm9p ZDpib3R0b21MZWZ0UmFkaXVzPSI3ZHAiPGJyPsKgwqDCoMKgwqAgYW5kcm9pZDpib3R0b21SaWdo dFJhZGl1cz0iN2RwIjxicj7CoMKgwqDCoMKgIGFuZHJvaWQ6dG9wTGVmdFJhZGl1cz0iN2RwIjxi cj7CoMKgwqDCoMKgIGFuZHJvaWQ6dG9wUmlnaHRSYWRpdXM9IjdkcCIgLyZndDs8YnI+PGJyPiZs dDsvc2hhcGUmZ3Q7PGJyPgpgYGA=">​</div>
</div>
<hr>
<h2>Creating the ViewHolder class<br>
</h2>
<h3><a name="TOC-Overview2"></a><span style="font-family:verdana,sans-serif">Overview</span></h3>
<p><span style="font-family:verdana,sans-serif">The ViewHolder class is used by the recylcer view to store layout information for each item in the RecyclerView list.&nbsp; Our implementation will define text view properties for each of our TextView controls, and provide list item positional information through an OnClick event.</span></p>
<h3><a name="TOC-Steps2"></a><span style="font-family:verdana,sans-serif">Steps</span></h3>
<font size="2">Create a new folder called 'Views' in DragDropDroid.&nbsp; Add a new class to the folder called ContactViewHolder with the following code:</font><br>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> System;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.Views;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.Widget;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.Support.V7.Widget;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> DragDropDroid.Views {
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">class</span> ContactViewHolder : RecyclerView.ViewHolder {
        Action&lt;<span style="color:rgb(51,51,51);font-weight:bold">int</span>&gt; _listener;

        <span style="color:rgb(51,51,51);font-weight:bold">public</span> TextView VCName { get; <span style="color:rgb(0,134,179)">set</span>; }
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> TextView VCEmail { get; <span style="color:rgb(0,134,179)">set</span>; }

        <span style="color:rgb(153,153,136);font-style:italic">// Initialize controls.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> ContactViewHolder(View itemView, Action&lt;<span style="color:rgb(51,51,51);font-weight:bold">int</span>&gt; listener) : base(itemView) {
            VCName = itemView.FindViewById&lt;TextView&gt;(Resource.Id.VCContactName);
            VCEmail = itemView.FindViewById&lt;TextView&gt;(Resource.Id.VCContactEmail);

            _listener = listener;
            itemView.Click += OnClick;
        }

        <span style="color:rgb(153,153,136);font-style:italic">// Provides positional information to the on click event.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">void</span> OnClick(object sender, EventArgs e) {
            <span style="color:rgb(51,51,51);font-weight:bold">int</span> position = base.AdapterPosition;
            <span style="color:rgb(51,51,51);font-weight:bold">if</span>(position != RecyclerView.NoPosition) {
                _listener(position);
            }
        }
    }
}</code></pre>
</div>
<hr>
<h2>Creating the Adapter class<br>
</h2>
<h3><a name="TOC-Overview2"></a><span style="font-family:verdana,sans-serif">Overview</span></h3>
<p><span style="font-family:verdana,sans-serif">To correctly build a functioning recycler view, you need implementations of a RecyclerView, Adapter, and ViewHolder classes.&nbsp; Of these 3-classes, the Adapter class typically requires the most customization.&nbsp; <br>
</span></p>
<p><span style="font-family:verdana,sans-serif">In this tutorial, our adapter class will hold the view model for our application because our main view won't even need to interact with it.&nbsp; The class will be responsible for binding the view holder to individual contacts in our view model.&nbsp; To support list item drag and drop, our adapter will inherit from IOnLongClickListener, which will enable us to set is as our CardView's long click listener.<br>
</span></p>
<h3><a name="TOC-Steps2"></a><span style="font-family:verdana,sans-serif">Steps</span></h3>
<font size="2"><span style="font-family:verdana,sans-serif">In the Views folder, create a class called ContactsAdapter.cs and add the following code:</span></font><br>
</div>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> System;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.Content;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.Views;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.Support.V7.Widget;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> DragDropDroid.ViewModels;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> <span style="color:rgb(51,51,51);font-weight:bold">static</span> Android.Views.View;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> DragDropDroid.Views {
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">class</span> ContactsAdapter : RecyclerView.Adapter, IOnLongClickListener {
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> event EventHandler&lt;<span style="color:rgb(51,51,51);font-weight:bold">int</span>&gt; ItemClick;

        <span style="color:rgb(51,51,51);font-weight:bold">public</span> MainViewModel MainViewModel;

        <span style="color:rgb(153,153,136);font-style:italic">// Simple ctor initializes a new instance of our MainViewModel.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> ContactsAdapter() {
            MainViewModel = <span style="color:rgb(51,51,51);font-weight:bold">new</span> MainViewModel();
        }

        <span style="color:rgb(153,153,136);font-style:italic">// Not used, but overridden as required.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> override <span style="color:rgb(51,51,51);font-weight:bold">int</span> ItemCount { get { <span style="color:rgb(51,51,51);font-weight:bold">return</span> MainViewModel.Contacts.Count; } }

        <span style="color:rgb(153,153,136);font-style:italic">// Set the layout and initialize the Card View's listener to this adapter's OnLoncClick implementation.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, <span style="color:rgb(51,51,51);font-weight:bold">int</span> viewType) {
            var layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ContactListItem, parent, <span style="color:rgb(51,51,51);font-weight:bold">false</span>);

            var vcContactsCardView = layout.FindViewById&lt;CardView&gt;(Resource.Id.VCContactCardView);
            vcContactsCardView.SetOnLongClickListener(<span style="color:rgb(51,51,51);font-weight:bold">this</span>);

            <span style="color:rgb(51,51,51);font-weight:bold">return</span> <span style="color:rgb(51,51,51);font-weight:bold">new</span> ContactViewHolder(layout, OnItemClick);
        }

        <span style="color:rgb(153,153,136);font-style:italic">// Bind the Name and Email fields to the Contacts list.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> override <span style="color:rgb(51,51,51);font-weight:bold">void</span> OnBindViewHolder(RecyclerView.ViewHolder holder, <span style="color:rgb(51,51,51);font-weight:bold">int</span> position) {
            var viewHolder = (ContactViewHolder)holder;

            viewHolder.VCName.Text = MainViewModel.Contacts[position].Name;
            viewHolder.VCEmail.Text = MainViewModel.Contacts[position].Email;
        }

        <span style="color:rgb(153,153,136);font-style:italic">// Invoke the OnItemClick event with positional data..</span>
        <span style="color:rgb(51,51,51);font-weight:bold">void</span> OnItemClick(<span style="color:rgb(51,51,51);font-weight:bold">int</span> position) {
            ItemClick?.Invoke(<span style="color:rgb(51,51,51);font-weight:bold">this</span>, position);
        }

        <span style="color:rgb(153,153,136);font-style:italic">// Long click event to start dragging when CarView has a LongClick event.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">bool</span> OnLongClick(View view) {
            ClipData data = ClipData.NewPlainText(<span style="color:rgb(221,17,68)">""</span>, <span style="color:rgb(221,17,68)">""</span>);
            DragShadowBuilder shadowBuilder = <span style="color:rgb(51,51,51);font-weight:bold">new</span> View.DragShadowBuilder(view);
            view.StartDrag(data, shadowBuilder, view, <span style="color:rgb(0,128,128)">0</span>);
            view.Visibility = ViewStates.Invisible;
            <span style="color:rgb(51,51,51);font-weight:bold">return</span> <span style="color:rgb(51,51,51);font-weight:bold">true</span>;
        }
    }
}
</code></pre>
<hr>
<h2>Creating the Main activity<br>
</h2>
<h3><a name="TOC-Overview2"></a><span style="font-family:verdana,sans-serif">Overview</span></h3>
<span style="font-family:verdana,sans-serif">The final step is to create the main Activity class.&nbsp; This class will be responsible for loading the main UI, initializing the recycler view components, and setting up the various view event handlers.&nbsp; This class will also define two drag event handlers <b>OnDragInsideRecyclerView</b> and <b>OnDragToBox</b>.&nbsp; <br>
<br>
</span></div>
<div><span style="font-family:verdana,sans-serif"><b>OnDragInsideRecyclerView</b> is responsible for re-arranging list view items inside the recycler view.&nbsp; We cannot use the drag helper methods for our recycler view because they do not support dragging list items outside of the RecyclerView view control.<br>
<br>
</span></div>
<div><span style="font-family:verdana,sans-serif"><b>OnDragToBox</b> is responsible for handling when list items are dragged to the box for deletion.&nbsp; It adjusts the box's style when contacts are dragged over it, and removes the contacts from the list when the user completes the drag action.</span><br>
</div>
<div>
<div>
<pre style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;font-size:1em;line-height:1.2em;margin:1.2em 0px"><code style="font-size:0.85em;font-family:Consolas,Inconsolata,Courier,monospace;margin:0px 0.15em;padding:0px 0.3em;white-space:pre-wrap;border:1px solid rgb(234,234,234);background-color:rgb(248,248,248);border-radius:3px;display:inline;white-space:pre;overflow:auto;border-radius:3px;border:1px solid rgb(204,204,204);padding:0.5em 0.7em;display:block!important;display:block;overflow-x:auto;padding:0.5em;color:rgb(51,51,51);background:rgb(248,248,248) none repeat scroll 0% 0%"><span style="color:rgb(51,51,51);font-weight:bold">using</span> System;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.App;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.Views;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.Widget;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.OS;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.Support.V7.Widget;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> <span style="color:rgb(51,51,51);font-weight:bold">static</span> Android.Views.View;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> Android.Graphics.Drawables;
<span style="color:rgb(51,51,51);font-weight:bold">using</span> DragDropDroid.Models;

<span style="color:rgb(51,51,51);font-weight:bold">namespace</span> DragDropDroid.Views {
    [Activity(Label = <span style="color:rgb(221,17,68)">"DragDropDroid"</span>, MainLauncher = <span style="color:rgb(51,51,51);font-weight:bold">true</span>, Icon = <span style="color:rgb(221,17,68)">"@drawable/icon"</span>)]
    <span style="color:rgb(51,51,51);font-weight:bold">public</span> <span style="color:rgb(51,51,51);font-weight:bold">class</span> MainView : Activity {

        RecyclerView _recyclerView;
        ContactsAdapter _adapter;

        Contact _contactToMove;
        <span style="color:rgb(51,51,51);font-weight:bold">int</span> _currentContactPosition = -<span style="color:rgb(0,128,128)">1</span>;
        <span style="color:rgb(51,51,51);font-weight:bold">int</span> _newContactPosition = -<span style="color:rgb(0,128,128)">1</span>;

        <span style="color:rgb(51,51,51);font-weight:bold">protected</span> override <span style="color:rgb(51,51,51);font-weight:bold">void</span> OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            <span style="color:rgb(153,153,136);font-style:italic">// Initialize the adapter.</span>
            _adapter = <span style="color:rgb(51,51,51);font-weight:bold">new</span> ContactsAdapter();
            _adapter.ItemClick += OnListItemClick;

            <span style="color:rgb(153,153,136);font-style:italic">// Initialize the recycler view.</span>
            _recyclerView = FindViewById&lt;RecyclerView&gt;(Resource.Id.VCContacts);
            _recyclerView.SetLayoutManager(<span style="color:rgb(51,51,51);font-weight:bold">new</span> LinearLayoutManager(<span style="color:rgb(51,51,51);font-weight:bold">this</span>, LinearLayoutManager.Vertical, <span style="color:rgb(51,51,51);font-weight:bold">false</span>));
            _recyclerView.SetAdapter(_adapter);

            <span style="color:rgb(153,153,136);font-style:italic">// Hook up our drag event hanlders to the Drop Area and Recycler View.</span>
            _recyclerView.Drag += OnDragInsideRecyclerView;
            FindViewById&lt;LinearLayout&gt;(Resource.Id.VCDropArea).Drag += OnDragToBox;

            <span style="color:rgb(153,153,136);font-style:italic">// Hook up the button click event.</span>
            FindViewById&lt;Button&gt;(Resource.Id.VCAddContact).Click += OnAddContactClick;
        }

        <span style="color:rgb(153,153,136);font-style:italic">// Adds a new contact to the contact list with some default values.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">private</span> <span style="color:rgb(51,51,51);font-weight:bold">void</span> OnAddContactClick(object sender, EventArgs e) {
             _adapter.MainViewModel.Contacts.Insert(<span style="color:rgb(0,128,128)">0</span>, <span style="color:rgb(51,51,51);font-weight:bold">new</span> Contact() { Name = <span style="color:rgb(221,17,68)">"Added  Contact"</span>, Email = <span style="color:rgb(221,17,68)">"AddedContact@fakeaddy.com"</span> });
            _adapter.NotifyItemInserted(<span style="color:rgb(0,128,128)">0</span>);
            _recyclerView.ScrollToPosition(<span style="color:rgb(0,128,128)">0</span>);
        }

        <span style="color:rgb(153,153,136);font-style:italic">// Displays an alert showing the selected contact's information.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">void</span> OnListItemClick(object sender, <span style="color:rgb(51,51,51);font-weight:bold">int</span> position) {
            var contact = _adapter.MainViewModel.Contacts[position];

            var builder = <span style="color:rgb(51,51,51);font-weight:bold">new</span> AlertDialog.Builder(<span style="color:rgb(51,51,51);font-weight:bold">this</span>);
            builder.SetTitle($<span style="color:rgb(221,17,68)">"{contact.Name}"</span>);
            builder.SetMessage($<span style="color:rgb(221,17,68)">"Email: {contact.Email}"</span>);
            builder.SetPositiveButton(<span style="color:rgb(221,17,68)">"OK"</span>, (send, args) =&gt; { });
            builder.SetCancelable(<span style="color:rgb(51,51,51);font-weight:bold">false</span>);
            builder.Show();
        }

        <span style="color:rgb(153,153,136);font-style:italic">// Rearranges items inside the recycler view based on drag actions.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">void</span> OnDragInsideRecyclerView(object sender, DragEventArgs e) {
            var theEvent = e.Event;
            var view = (View)theEvent.LocalState;
            var containerView = (RecyclerView)sender;

            _currentContactPosition = containerView.GetChildAdapterPosition(view);

            <span style="color:rgb(153,153,136);font-style:italic">// Ensure the position is valid.</span>
            <span style="color:rgb(51,51,51);font-weight:bold">if</span>(_currentContactPosition != -<span style="color:rgb(0,128,128)">1</span>) {
                _contactToMove = _adapter.MainViewModel.Contacts[_currentContactPosition];
            }

            <span style="color:rgb(51,51,51);font-weight:bold">switch</span>(theEvent.Action) {
                <span style="color:rgb(51,51,51);font-weight:bold">case</span> DragAction.Location:
                    View onTopOf = containerView.FindChildViewUnder(theEvent.GetX(), theEvent.GetY());
                    _newContactPosition = containerView.GetChildAdapterPosition(onTopOf);

                    <span style="color:rgb(153,153,136);font-style:italic">// Ensure the new position is valid.</span>
                    <span style="color:rgb(51,51,51);font-weight:bold">if</span>(_newContactPosition != -<span style="color:rgb(0,128,128)">1</span>) {

                        var layoutManager = (LinearLayoutManager)_recyclerView.GetLayoutManager();
                        <span style="color:rgb(51,51,51);font-weight:bold">int</span> firstVisiblePosition = layoutManager.FindFirstCompletelyVisibleItemPosition();
                        <span style="color:rgb(51,51,51);font-weight:bold">int</span> lastVisiblePosition = layoutManager.FindLastVisibleItemPosition();

                        <span style="color:rgb(153,153,136);font-style:italic">// Scroll up or down one if we are over the first or last visible list item.</span>
                        <span style="color:rgb(51,51,51);font-weight:bold">if</span>(_newContactPosition &gt;= lastVisiblePosition)
                            layoutManager.ScrollToPositionWithOffset(firstVisiblePosition + <span style="color:rgb(0,128,128)">1</span>, <span style="color:rgb(0,128,128)">0</span>);
                        <span style="color:rgb(51,51,51);font-weight:bold">else</span> <span style="color:rgb(51,51,51);font-weight:bold">if</span>(_newContactPosition &lt;= firstVisiblePosition)
                            layoutManager.ScrollToPositionWithOffset(firstVisiblePosition - <span style="color:rgb(0,128,128)">1</span>, <span style="color:rgb(0,128,128)">0</span>);

                        <span style="color:rgb(153,153,136);font-style:italic">// Update the location of the Contact</span>
                        _adapter.MainViewModel.Contacts.Remove(_contactToMove);
                        _adapter.MainViewModel.Contacts.Insert(_newContactPosition, _contactToMove);
                        _adapter.NotifyItemMoved(_currentContactPosition, _newContactPosition);
                    }
                    e.Handled = <span style="color:rgb(51,51,51);font-weight:bold">true</span>;
                    <span style="color:rgb(51,51,51);font-weight:bold">break</span>;

                <span style="color:rgb(51,51,51);font-weight:bold">case</span> DragAction.Ended:
                    <span style="color:rgb(153,153,136);font-style:italic">// Reset the visibility for the Contact item's view.</span>
                    <span style="color:rgb(153,153,136);font-style:italic">// This is done to reset the state in instances where the drag action didn't do anything.</span>
                    view.Visibility = ViewStates.Visible;

                    <span style="color:rgb(153,153,136);font-style:italic">// Boundary condition, scroll to top is moving list item to position 0.</span>
                    <span style="color:rgb(51,51,51);font-weight:bold">if</span>(_newContactPosition != -<span style="color:rgb(0,128,128)">1</span>) {
                        _recyclerView.ScrollToPosition(_newContactPosition);
                        _newContactPosition = -<span style="color:rgb(0,128,128)">1</span>;
                    }
                    <span style="color:rgb(51,51,51);font-weight:bold">else</span> {
                        _recyclerView.ScrollToPosition(<span style="color:rgb(0,128,128)">0</span>);
                    }

                    e.Handled = <span style="color:rgb(51,51,51);font-weight:bold">true</span>;
                    <span style="color:rgb(51,51,51);font-weight:bold">break</span>;
            }
        }

        <span style="color:rgb(153,153,136);font-style:italic">// Sets drag box style when contacts are dragged to the box.</span>
        <span style="color:rgb(153,153,136);font-style:italic">// Removes list items from recycler view when user drags list items to the box.</span>
        <span style="color:rgb(51,51,51);font-weight:bold">void</span> OnDragToBox(object sender, DragEventArgs e) {
            <span style="color:rgb(153,153,136);font-style:italic">// Styling updates based on whether the drag item is in our out of the box area.</span>
            Drawable defaultShape = GetDrawable(Resource.Drawable.shape);
            Drawable enterShape = GetDrawable(Resource.Drawable.shape_droptarget);

            var theEvent = e.Event;
            var view = (View)theEvent.LocalState;
            var containerView = (View)sender;

            <span style="color:rgb(51,51,51);font-weight:bold">switch</span>(theEvent.Action) {
                <span style="color:rgb(51,51,51);font-weight:bold">case</span> DragAction.Entered:
                    containerView.SetBackgroundResource(Resource.Drawable.shape_droptarget);
                    e.Handled = <span style="color:rgb(51,51,51);font-weight:bold">true</span>;
                    <span style="color:rgb(51,51,51);font-weight:bold">break</span>;

                <span style="color:rgb(51,51,51);font-weight:bold">case</span> DragAction.Exited:
                    containerView.SetBackgroundResource(Resource.Drawable.shape);
                    e.Handled = <span style="color:rgb(51,51,51);font-weight:bold">true</span>;
                    <span style="color:rgb(51,51,51);font-weight:bold">break</span>;

                <span style="color:rgb(51,51,51);font-weight:bold">case</span> DragAction.Drop:

                    _adapter.MainViewModel.Contacts.Remove(_contactToMove);
                    _adapter.NotifyItemRemoved(_currentContactPosition);

                    ViewGroup owner = (ViewGroup)(view.Parent);
                    <span style="color:rgb(51,51,51);font-weight:bold">if</span>(owner != null)
                        owner.RemoveView(view);
                    e.Handled = <span style="color:rgb(51,51,51);font-weight:bold">true</span>;
                    <span style="color:rgb(51,51,51);font-weight:bold">break</span>;

                <span style="color:rgb(51,51,51);font-weight:bold">case</span> DragAction.Ended:
                    containerView.SetBackgroundResource(Resource.Drawable.shape);
                    e.Handled = <span style="color:rgb(51,51,51);font-weight:bold">true</span>;
                    <span style="color:rgb(51,51,51);font-weight:bold">break</span>;
            }
        }
    }
}
</code></pre>
<div style="height:0;width:0;max-height:0;max-width:0;overflow:hidden;font-size:0em;padding:0;margin:0" title="MDH:YGBgQysrPGJyPnVzaW5nIFN5c3RlbTs8YnI+dXNpbmcgQW5kcm9pZC5BcHA7PGJyPnVzaW5nIEFu ZHJvaWQuVmlld3M7PGJyPnVzaW5nIEFuZHJvaWQuV2lkZ2V0Ozxicj51c2luZyBBbmRyb2lkLk9T Ozxicj51c2luZyBBbmRyb2lkLlN1cHBvcnQuVjcuV2lkZ2V0Ozxicj51c2luZyBzdGF0aWMgQW5k cm9pZC5WaWV3cy5WaWV3Ozxicj51c2luZyBBbmRyb2lkLkdyYXBoaWNzLkRyYXdhYmxlczs8YnI+ dXNpbmcgRHJhZ0Ryb3BEcm9pZC5Nb2RlbHM7PGJyPjxicj5uYW1lc3BhY2UgRHJhZ0Ryb3BEcm9p ZC5WaWV3cyB7PGJyPsKgwqDCoCBbQWN0aXZpdHkoTGFiZWwgPSAiRHJhZ0Ryb3BEcm9pZCIsIE1h aW5MYXVuY2hlciA9IHRydWUsIEljb24gPSAiQGRyYXdhYmxlL2ljb24iKV08YnI+wqDCoMKgIHB1 YmxpYyBjbGFzcyBNYWluVmlldyA6IEFjdGl2aXR5IHs8YnI+PGJyPsKgwqDCoMKgwqDCoMKgIFJl Y3ljbGVyVmlldyBfcmVjeWNsZXJWaWV3Ozxicj7CoMKgwqDCoMKgwqDCoCBDb250YWN0c0FkYXB0 ZXIgX2FkYXB0ZXI7PGJyPjxicj7CoMKgwqDCoMKgwqDCoCBDb250YWN0IF9jb250YWN0VG9Nb3Zl Ozxicj7CoMKgwqDCoMKgwqDCoCBpbnQgX2N1cnJlbnRDb250YWN0UG9zaXRpb24gPSAtMTs8YnI+ wqDCoMKgwqDCoMKgwqAgaW50IF9uZXdDb250YWN0UG9zaXRpb24gPSAtMTs8YnI+PGJyPsKgwqDC oMKgwqDCoMKgIHByb3RlY3RlZCBvdmVycmlkZSB2b2lkIE9uQ3JlYXRlKEJ1bmRsZSBidW5kbGUp IHs8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBiYXNlLk9uQ3JlYXRlKGJ1bmRsZSk7PGJyPsKg wqDCoMKgwqDCoMKgwqDCoMKgwqAgU2V0Q29udGVudFZpZXcoUmVzb3VyY2UuTGF5b3V0Lk1haW4p Ozxicj48YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoCAvLyBJbml0aWFsaXplIHRoZSBhZGFwdGVy Ljxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIF9hZGFwdGVyID0gbmV3IENvbnRhY3RzQWRhcHRl cigpOzxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIF9hZGFwdGVyLkl0ZW1DbGljayArPSBPbkxp c3RJdGVtQ2xpY2s7PGJyPjxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIC8vIEluaXRpYWxpemUg dGhlIHJlY3ljbGVyIHZpZXcuPGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqAgX3JlY3ljbGVyVmll dyA9IEZpbmRWaWV3QnlJZCZsdDtSZWN5Y2xlclZpZXcmZ3Q7KFJlc291cmNlLklkLlZDQ29udGFj dHMpOzxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIF9yZWN5Y2xlclZpZXcuU2V0TGF5b3V0TWFu YWdlcihuZXcgTGluZWFyTGF5b3V0TWFuYWdlcih0aGlzLCBMaW5lYXJMYXlvdXRNYW5hZ2VyLlZl cnRpY2FsLCBmYWxzZSkpOzxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIF9yZWN5Y2xlclZpZXcu U2V0QWRhcHRlcihfYWRhcHRlcik7PGJyPjxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIC8vIEhv b2sgdXAgb3VyIGRyYWcgZXZlbnQgaGFubGRlcnMgdG8gdGhlIERyb3AgQXJlYSBhbmQgUmVjeWNs ZXIgVmlldy48YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBfcmVjeWNsZXJWaWV3LkRyYWcgKz0g T25EcmFnSW5zaWRlUmVjeWNsZXJWaWV3Ozxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIEZpbmRW aWV3QnlJZCZsdDtMaW5lYXJMYXlvdXQmZ3Q7KFJlc291cmNlLklkLlZDRHJvcEFyZWEpLkRyYWcg Kz0gT25EcmFnVG9Cb3g7PGJyPjxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIC8vIEhvb2sgdXAg dGhlIGJ1dHRvbiBjbGljayBldmVudC48YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBGaW5kVmll d0J5SWQmbHQ7QnV0dG9uJmd0OyhSZXNvdXJjZS5JZC5WQ0FkZENvbnRhY3QpLkNsaWNrICs9IE9u QWRkQ29udGFjdENsaWNrOzxicj7CoMKgwqDCoMKgwqDCoCB9PGJyPjxicj7CoMKgwqDCoMKgwqDC oCAvLyBBZGRzIGEgbmV3IGNvbnRhY3QgdG8gdGhlIGNvbnRhY3QgbGlzdCB3aXRoIHNvbWUgZGVm YXVsdCB2YWx1ZXMuPGJyPsKgwqDCoMKgwqDCoMKgIHByaXZhdGUgdm9pZCBPbkFkZENvbnRhY3RD bGljayhvYmplY3Qgc2VuZGVyLCBFdmVudEFyZ3MgZSkgezxicj7CoMKgwqDCoMKgwqDCoMKgwqDC oMKgCiBfYWRhcHRlci5NYWluVmlld01vZGVsLkNvbnRhY3RzLkluc2VydCgwLCBuZXcgQ29udGFj dCgpIHsgTmFtZSA9ICJBZGRlZAogQ29udGFjdCIsIEVtYWlsID0gIkFkZGVkQ29udGFjdEBmYWtl YWRkeS5jb20iIH0pOzxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIF9hZGFwdGVyLk5vdGlmeUl0 ZW1JbnNlcnRlZCgwKTs8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBfcmVjeWNsZXJWaWV3LlNj cm9sbFRvUG9zaXRpb24oMCk7PGJyPsKgwqDCoMKgwqDCoMKgIH08YnI+PGJyPsKgwqDCoMKgwqDC oMKgIC8vIERpc3BsYXlzIGFuIGFsZXJ0IHNob3dpbmcgdGhlIHNlbGVjdGVkIGNvbnRhY3QncyBp bmZvcm1hdGlvbi48YnI+wqDCoMKgwqDCoMKgwqAgdm9pZCBPbkxpc3RJdGVtQ2xpY2sob2JqZWN0 IHNlbmRlciwgaW50IHBvc2l0aW9uKSB7PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqAgdmFyIGNv bnRhY3QgPSBfYWRhcHRlci5NYWluVmlld01vZGVsLkNvbnRhY3RzW3Bvc2l0aW9uXTs8YnI+PGJy PsKgwqDCoMKgwqDCoMKgwqDCoMKgwqAgdmFyIGJ1aWxkZXIgPSBuZXcgQWxlcnREaWFsb2cuQnVp bGRlcih0aGlzKTs8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBidWlsZGVyLlNldFRpdGxlKCQi e2NvbnRhY3QuTmFtZX0iKTs8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBidWlsZGVyLlNldE1l c3NhZ2UoJCJFbWFpbDoge2NvbnRhY3QuRW1haWx9Iik7PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKg wqAgYnVpbGRlci5TZXRQb3NpdGl2ZUJ1dHRvbigiT0siLCAoc2VuZCwgYXJncykgPSZndDsgeyB9 KTs8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBidWlsZGVyLlNldENhbmNlbGFibGUoZmFsc2Up Ozxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIGJ1aWxkZXIuU2hvdygpOzxicj7CoMKgwqDCoMKg wqDCoCB9PGJyPjxicj7CoMKgwqDCoMKgwqDCoCAvLyBSZWFycmFuZ2VzIGl0ZW1zIGluc2lkZSB0 aGUgcmVjeWNsZXIgdmlldyBiYXNlZCBvbiBkcmFnIGFjdGlvbnMuPGJyPsKgwqDCoMKgwqDCoMKg IHZvaWQgT25EcmFnSW5zaWRlUmVjeWNsZXJWaWV3KG9iamVjdCBzZW5kZXIsIERyYWdFdmVudEFy Z3MgZSkgezxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIHZhciB0aGVFdmVudCA9IGUuRXZlbnQ7 PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqAgdmFyIHZpZXcgPSAoVmlldyl0aGVFdmVudC5Mb2Nh bFN0YXRlOzxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIHZhciBjb250YWluZXJWaWV3ID0gKFJl Y3ljbGVyVmlldylzZW5kZXI7PGJyPjxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIF9jdXJyZW50 Q29udGFjdFBvc2l0aW9uID0gY29udGFpbmVyVmlldy5HZXRDaGlsZEFkYXB0ZXJQb3NpdGlvbih2 aWV3KTs8YnI+PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqAgLy8gRW5zdXJlIHRoZSBwb3NpdGlv biBpcyB2YWxpZC48YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBpZihfY3VycmVudENvbnRhY3RQ b3NpdGlvbiAhPSAtMSkgezxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqAgX2NvbnRh Y3RUb01vdmUgPSBfYWRhcHRlci5NYWluVmlld01vZGVsLkNvbnRhY3RzW19jdXJyZW50Q29udGFj dFBvc2l0aW9uXTs8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoCB9PGJyPjxicj48YnI+wqDCoMKg wqDCoMKgwqDCoMKgwqDCoCBzd2l0Y2godGhlRXZlbnQuQWN0aW9uKSB7PGJyPsKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqDCoCBjYXNlIERyYWdBY3Rpb24uTG9jYXRpb246PGJyPsKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgIFZpZXcgb25Ub3BPZiA9IGNvbnRhaW5lclZp ZXcuRmluZENoaWxkVmlld1VuZGVyKHRoZUV2ZW50LkdldFgoKSwgdGhlRXZlbnQuR2V0WSgpKTs8 YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqAgX25ld0NvbnRhY3RQb3Np dGlvbiA9IGNvbnRhaW5lclZpZXcuR2V0Q2hpbGRBZGFwdGVyUG9zaXRpb24ob25Ub3BPZik7PGJy Pjxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCAvLyBFbnN1cmUgdGhl IG5ldyBwb3NpdGlvbiBpcyB2YWxpZC48YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqAgaWYoX25ld0NvbnRhY3RQb3NpdGlvbiAhPSAtMSkgezxicj48YnI+wqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCB2YXIgbGF5b3V0TWFuYWdlciA9 IChMaW5lYXJMYXlvdXRNYW5hZ2VyKV9yZWN5Y2xlclZpZXcuR2V0TGF5b3V0TWFuYWdlcigpOzxi cj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgIGludCBmaXJz dFZpc2libGVQb3NpdGlvbiA9IGxheW91dE1hbmFnZXIuRmluZEZpcnN0Q29tcGxldGVseVZpc2li bGVJdGVtUG9zaXRpb24oKTs8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqDCoCBpbnQgbGFzdFZpc2libGVQb3NpdGlvbiA9IGxheW91dE1hbmFnZXIuRmluZExh c3RWaXNpYmxlSXRlbVBvc2l0aW9uKCk7PGJyPjxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqDCoMKgIC8vIFNjcm9sbCB1cCBvciBkb3duIG9uZSBpZiB3ZSBhcmUg b3ZlciB0aGUgZmlyc3Qgb3IgbGFzdCB2aXNpYmxlIGxpc3QgaXRlbS48YnI+wqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBpZihfbmV3Q29udGFjdFBvc2l0aW9u ICZndDs9IGxhc3RWaXNpYmxlUG9zaXRpb24pPGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBsYXlvdXRNYW5hZ2VyLlNjcm9sbFRvUG9zaXRp b25XaXRoT2Zmc2V0KGZpcnN0VmlzaWJsZVBvc2l0aW9uICsgMSwgMCk7PGJyPsKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqAgZWxzZSBpZihfbmV3Q29udGFjdFBv c2l0aW9uICZsdDs9IGZpcnN0VmlzaWJsZVBvc2l0aW9uKTxicj7CoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqAgbGF5b3V0TWFuYWdlci5TY3JvbGxU b1Bvc2l0aW9uV2l0aE9mZnNldChmaXJzdFZpc2libGVQb3NpdGlvbiAtIDEsIDApOzxicj48YnI+ wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCAvLyBVcGRhdGUg dGhlIGxvY2F0aW9uIG9mIHRoZSBDb250YWN0PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgwqAgX2FkYXB0ZXIuTWFpblZpZXdNb2RlbC5Db250YWN0cy5SZW1v dmUoX2NvbnRhY3RUb01vdmUpOzxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgIF9hZGFwdGVyLk1haW5WaWV3TW9kZWwuQ29udGFjdHMuSW5zZXJ0KF9uZXdD b250YWN0UG9zaXRpb24sIF9jb250YWN0VG9Nb3ZlKTs8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBfYWRhcHRlci5Ob3RpZnlJdGVtTW92ZWQoX2N1cnJl bnRDb250YWN0UG9zaXRpb24sIF9uZXdDb250YWN0UG9zaXRpb24pOzxicj7CoMKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCB9PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgIGUuSGFuZGxlZCA9IHRydWU7PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgIGJyZWFrOzxicj48YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgIGNhc2UgRHJhZ0FjdGlvbi5FbmRlZDo8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqDCoMKgwqAgLy8gUmVzZXQgdGhlIHZpc2liaWxpdHkgZm9yIHRoZSBDb250YWN0IGl0ZW0n cyB2aWV3Ljxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCAvLyBUaGlz IGlzIGRvbmUgdG8gcmVzZXQgdGhlIHN0YXRlIGluIGluc3RhbmNlcyB3aGVyZSB0aGUgZHJhZyBh Y3Rpb24gZGlkbid0IGRvIGFueXRoaW5nLjxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqDCoCB2aWV3LlZpc2liaWxpdHkgPSBWaWV3U3RhdGVzLlZpc2libGU7PGJyPjxicj7C oMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCAvLyBCb3VuZGFyeSBjb25kaXRp b24sIHNjcm9sbCB0byB0b3AgaXMgbW92aW5nIGxpc3QgaXRlbSB0byBwb3NpdGlvbiAwLjxicj7C oMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBpZihfbmV3Q29udGFjdFBvc2l0 aW9uICE9IC0xKSB7PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqAgX3JlY3ljbGVyVmlldy5TY3JvbGxUb1Bvc2l0aW9uKF9uZXdDb250YWN0UG9zaXRpb24p Ozxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgIF9uZXdD b250YWN0UG9zaXRpb24gPSAtMTs8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqAgfTxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBlbHNlIHs8 YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBfcmVjeWNs ZXJWaWV3LlNjcm9sbFRvUG9zaXRpb24oMCk7PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgIH08YnI+PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgIGUuSGFuZGxlZCA9IHRydWU7PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgIGJyZWFrOzxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIH08YnI+wqDCoMKgwqDCoMKg wqAgfTxicj48YnI+wqDCoMKgwqDCoMKgwqAgLy8gU2V0cyBkcmFnIGJveCBzdHlsZSB3aGVuIGNv bnRhY3RzIGFyZSBkcmFnZ2VkIHRvIHRoZSBib3guPGJyPsKgwqDCoMKgwqDCoMKgIC8vIFJlbW92 ZXMgbGlzdCBpdGVtcyBmcm9tIHJlY3ljbGVyIHZpZXcgd2hlbiB1c2VyIGRyYWdzIGxpc3QgaXRl bXMgdG8gdGhlIGJveC48YnI+wqDCoMKgwqDCoMKgwqAgdm9pZCBPbkRyYWdUb0JveChvYmplY3Qg c2VuZGVyLCBEcmFnRXZlbnRBcmdzIGUpIHs8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDCoCAvLyBT dHlsaW5nIHVwZGF0ZXMgYmFzZWQgb24gd2hldGhlciB0aGUgZHJhZyBpdGVtIGlzIGluIG91ciBv dXQgb2YgdGhlIGJveCBhcmVhLjxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgIERyYXdhYmxlIGRl ZmF1bHRTaGFwZSA9IEdldERyYXdhYmxlKFJlc291cmNlLkRyYXdhYmxlLnNoYXBlKTs8YnI+wqDC oMKgwqDCoMKgwqDCoMKgwqDCoCBEcmF3YWJsZSBlbnRlclNoYXBlID0gR2V0RHJhd2FibGUoUmVz b3VyY2UuRHJhd2FibGUuc2hhcGVfZHJvcHRhcmdldCk7PGJyPjxicj7CoMKgwqDCoMKgwqDCoMKg wqDCoMKgIHZhciB0aGVFdmVudCA9IGUuRXZlbnQ7PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqAg dmFyIHZpZXcgPSAoVmlldyl0aGVFdmVudC5Mb2NhbFN0YXRlOzxicj7CoMKgwqDCoMKgwqDCoMKg wqDCoMKgIHZhciBjb250YWluZXJWaWV3ID0gKFZpZXcpc2VuZGVyOzxicj48YnI+wqDCoMKgwqDC oMKgwqDCoMKgwqDCoCBzd2l0Y2godGhlRXZlbnQuQWN0aW9uKSB7PGJyPsKgwqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoCBjYXNlIERyYWdBY3Rpb24uRW50ZXJlZDo8YnI+wqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqAgY29udGFpbmVyVmlldy5TZXRCYWNrZ3JvdW5kUmVz b3VyY2UoUmVzb3VyY2UuRHJhd2FibGUuc2hhcGVfZHJvcHRhcmdldCk7PGJyPsKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgIGUuSGFuZGxlZCA9IHRydWU7PGJyPsKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgIGJyZWFrOzxicj48YnI+wqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgIGNhc2UgRHJhZ0FjdGlvbi5FeGl0ZWQ6PGJyPsKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgIGNvbnRhaW5lclZpZXcuU2V0QmFja2dyb3VuZFJl c291cmNlKFJlc291cmNlLkRyYXdhYmxlLnNoYXBlKTs8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqAgZS5IYW5kbGVkID0gdHJ1ZTs8YnI+wqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgwqAgYnJlYWs7PGJyPjxicj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqAgY2FzZSBEcmFnQWN0aW9uLkRyb3A6PGJyPjxicj7CoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqDCoCBfYWRhcHRlci5NYWluVmlld01vZGVsLkNvbnRhY3RzLlJlbW92 ZShfY29udGFjdFRvTW92ZSk7PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgIF9hZGFwdGVyLk5vdGlmeUl0ZW1SZW1vdmVkKF9jdXJyZW50Q29udGFjdFBvc2l0aW9uKTs8 YnI+PGJyPsKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgIFZpZXdHcm91cCBv d25lciA9IChWaWV3R3JvdXApKHZpZXcuUGFyZW50KTs8YnI+wqDCoMKgwqDCoMKgwqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqAgaWYob3duZXIgIT0gbnVsbCk8YnI+wqDCoMKgwqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBvd25lci5SZW1vdmVWaWV3KHZpZXcpOzxicj7C oMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBlLkhhbmRsZWQgPSB0cnVlOzxi cj7CoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBicmVhazs8YnI+PGJyPsKg wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoCBjYXNlIERyYWdBY3Rpb24uRW5kZWQ6PGJyPsKg wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgIGNvbnRhaW5lclZpZXcuU2V0QmFj a2dyb3VuZFJlc291cmNlKFJlc291cmNlLkRyYXdhYmxlLnNoYXBlKTs8YnI+wqDCoMKgwqDCoMKg wqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqAgZS5IYW5kbGVkID0gdHJ1ZTs8YnI+wqDCoMKgwqDC oMKgwqDCoMKgwqDCoMKgwqDCoMKgwqDCoMKgwqAgYnJlYWs7PGJyPsKgwqDCoMKgwqDCoMKgwqDC oMKgwqAgfTxicj7CoMKgwqDCoMKgwqDCoCB9PGJyPsKgwqDCoCB9PGJyPn08YnI+CmBgYA==">​</div>
</div>
<hr>
<h2>Conclusion</h2>
<span style="font-family:verdana,sans-serif">At this point you should be
 able to build and run the DragDropDroid project and test out the custom drag and drop functionality.&nbsp; Dragging contacts inside the recycler view allows you to rearrange them, and dragging items to the delete box removes them from the list.<br>
<br>
Below are screenshots of the application running on an Android simulator</span><b><span style="font-family:verdana,sans-serif"><br>
<div style="display:block;text-align:left"><a href="https://sites.google.com/site/netdeveloperblog/xamarin/android/customdragdrop/Screenshots.png?attredirects=0" imageanchor="1"><img src="https://sites.google.com/site/netdeveloperblog/xamarin/android/customdragdrop/Screenshots.png" style="width:100%" border="0"></a></div>
<br>
</span></b><span style="font-family:verdana,sans-serif">I truly hope you found this tutorial to be both clear and informative, and please feel free to leave feedback.<br>
<br>
</span>
<hr>
<h2>Source Code</h2>
<p><span style="font-family:verdana,sans-serif">On GitHub: <a href="https://github.com/C0D3Name/DragDropDroid" target="_blank">https://github.com/C0D3Name/DragDropDroid</a></span></p>
<span style="font-family:verdana,sans-serif">The
 source code for this tutorial series can be found on my GitHub page.</span><b><span style="font-family:verdana,sans-serif">
</span></b></div>
<div><font size="2"><span style="font-family:verdana,sans-serif"><br>
</span></font></div>
<div></div>
</div>
