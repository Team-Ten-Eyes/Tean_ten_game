using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using Game.Models;
using Game.Views;

namespace Game.ViewModels
{
    public class CharacterViewModel<T> : BaseViewModel<DefaultModel> where T : class
    {
        public T Data { get; set; }
        public CharacterViewModel(T data)
        {
            if (data != null)
            {
                Title = (data as BaseModel<T>).Name;
            }
            Data = data;
        }

         

    }
}
