using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Comet;

namespace Tasky
{
    public class MainPage : View
    {

        readonly State<ObservableCollection<TodoItem>> items = new ObservableCollection<TodoItem>{
            new TodoItem{
                Name = "Hi",
                Done = true,
            },
            new TodoItem
            {
                Name ="Finish Tasky",
            }
        };

        public readonly State<bool> isAdding = false;

        [Body]
        View body()
        {

            var content = isAdding.Value ? (View)
             new HStack(spacing: 6) {
                new NewTodoView()
                .SetEnvironment(nameof(isAdding), isAdding)
                .SetEnvironment(nameof(items), items)
            }
             : new ListView<TodoItem>(items.Value)
             {
                 ViewFor = (item) => new VStack{
                    new HStack
                    {
                        new Text(item.Name).Frame(alignment: Alignment.Leading),
                        new Spacer(),
                        new Toggle(item.Done).Frame(alignment:Alignment.Center)
                    }.Padding(6)
                }.FillHorizontal()
             };

            return new NavigationView{
                content
                .Title("Tasky")
            }.SetRightActionButton(new Tuple<string, Action>("+", AddItem));
        }

        void AddItem()
        {
            isAdding.Value = true;
        }

        public class BorderedEntry : View
        {
            private Binding<String> _val;
            private string _placeholder;

            public BorderedEntry(Binding<String> val, string placeholder)
            {
                _val = val;
                _placeholder = placeholder;
            }

            [Body]
            View body() => new VStack(spacing: 8)
            {
                new TextField(_val, _placeholder).FillHorizontal().Padding(6)
            }
            .Frame(height: 40)
            .RoundedBorder(color: Color.Grey);
        }

        public class NewTodoView : View
        {
            [Environment]
            readonly State<bool> isAdding = false;

            [Environment]
            readonly ObservableCollection<TodoItem> items;

            readonly State<string> newItem = "";
            readonly State<bool> errorIsVisible = false;

            [Body]
            View body()
            {
                var view = new VStack()
                {
                    new BorderedEntry(newItem, "Task Name").FillHorizontal().Padding(6),

                    new Button("Add", () =>
                    {
                        if (string.IsNullOrWhiteSpace(newItem.Value))
                        {
                            errorIsVisible.Value = true;
                            return;
                        }

                        items.Add(new TodoItem { Name = newItem.Value });
                        isAdding.Value = false;
                    }),
                    new Button("Cancel", () =>
                    {
                        isAdding.Value = false;
                    }),
                    new Text("Name cannot be empty").Color(errorIsVisible ? Color.Red : Color.Transparent)
                };

                return view;
            }
        }
    }
}
