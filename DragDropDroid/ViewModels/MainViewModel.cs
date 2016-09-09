using System.Collections.Generic;
using DragDropDroid.Models;

namespace DragDropDroid.ViewModels {
    public class MainViewModel {
        // List of contacts Adapter will bind to.
        public List<Contact> Contacts = new List<Contact>();

        public MainViewModel() {
            CreateSampleData();
        }

        // Create some sample data to mess around with.
        private void CreateSampleData() {
            if(Contacts.Count < 1) {
                for( int ii = 1; ii < 10; ii++) {
                    Contacts.Add(
                        new Contact() {
                            Name = $"Person {ii}",
                            Email = $"Person{ii}@fakeaddy.com"
                        });
                }
            }
        }
    }
}