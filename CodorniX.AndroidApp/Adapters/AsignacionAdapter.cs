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
    class AsignacionAdapter : ArrayAdapter<Periodo>
    {
        private class ViewHolder : Java.Lang.Object
        {
            public TextView Departamento;
            public TextView Turno;
        }

        public AsignacionAdapter(Context context, IList<Periodo> objects) : base(context, 0, objects)
        {

        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Periodo periodo = GetItem(position);
            ViewHolder holder = null;

            if (convertView != null)
                holder = convertView.Tag as ViewHolder;

            if (holder == null)
            {
                holder = new ViewHolder();
                convertView = LayoutInflater.From(Context).Inflate(Resource.Layout.item_asignacion, null);
                holder.Departamento = convertView.FindViewById<TextView>(Resource.Id.tvDepartamento);
                holder.Turno = convertView.FindViewById<TextView>(Resource.Id.tvTurno);
                convertView.Tag = holder;
            }

            holder.Departamento.Text = periodo.StrNombreDepto;
            holder.Turno.Text = periodo.StrTurno;

            return convertView;
        }
    }
}