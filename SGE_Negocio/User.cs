using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE_Entity;
using SGE_Names;

namespace SGE_Datos.Orm
{
    public class User
    {
        public void Create(Ent_User ent_User)
        {
            Conexion.GDatos.Ejecutar(NamesProcedures.InsertUser , ent_User.USE_GUID_ID,
                                                                  ent_User.PER_GUID_ID, 
                                                                  ent_User.STD_GUID_ID,
                                                                  ent_User.USE_LOGIN,
                                                                  ent_User.USE_PASS,
                                                                  ent_User.USE_STATUS
                                                        );


        }
    }
}
