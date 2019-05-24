using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CodorniX.Modelo;

namespace CodorniX.AndroidApp.Adapters
{
    class TareaAdapter : ArrayAdapter<Tarea>
    {
        private class ViewHolder : Java.Lang.Object
        {
            public TextView Tarea;
            public TextView Departamento;
            public TextView Turno;
        }

        public TareaAdapter(Context context, IList<Tarea> objects) : base(context, 0, objects)
        {

        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Tarea tarea = GetItem(position);
            ViewHolder holder = null;

            if (convertView != null)
                holder = convertView.Tag as ViewHolder;

            if (holder == null)
            {
                holder = new ViewHolder();
                convertView = LayoutInflater.From(Context).Inflate(Resource.Layout.item_tarea, null);
                holder.Tarea = convertView.FindViewById<TextView>(Resource.Id.tvTarea);
                holder.Departamento = convertView.FindViewById<TextView>(Resource.Id.tvDepartamento);
                holder.Turno = convertView.FindViewById<TextView>(Resource.Id.tvTurno);
                convertView.Tag = holder;
            }

            holder.Tarea.Text = tarea.StrNombre;
            holder.Departamento.Text = tarea.StrDepartamento;
            holder.Turno.Text = tarea.StrTurno;

            return convertView;
        }
    }
}