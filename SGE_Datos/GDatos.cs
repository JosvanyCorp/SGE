using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SGE_Conexion
{
    public abstract class GDatos
    {
        #region "Declaración de Variables"

        protected string ServidorValue = "";
        protected string BaseDatosValue = "";
        protected string UsuarioValue = "";
        protected string PasswordValue = "";
        protected string CadenaConexionValue = "";
        protected IDbConnection ConexionValue;

        #endregion

        #region "Establecer y Obtener"

        // Nombre del equipo servidor de datos.
        public string Servidor
        {
            get
            {
                return ServidorValue;
            }
            set
            {
                ServidorValue = value;
            }
        } // end Servidor

        // Nombre de la base de datos a utilizar.
        public string Base
        {
            get
            {
                return BaseDatosValue;
            }
            set
            {
                BaseDatosValue = value;
            }
        } // end Base

        // Nombre del Usuario de la BD.
        public string Usuario
        {
            get
            {
                return UsuarioValue;
            }
            set
            {
                UsuarioValue = value;
            }
        } // end Usuario

        // Password del Usuario de la BD.
        public string Password
        {
            get
            {
                return PasswordValue;
            }
            set
            {
                PasswordValue = value;
            }
        } // end Password

        // Cadena de conexión completa a la base.
        public abstract string CadenaConexion
        {
            get;
            set;
        }

        #endregion

        #region "Privadas"

        // Crea u obtiene un objeto para conectarse a la base de datos.
        protected IDbConnection Conexion
        {
            get
            {
                // si aun no tiene asignada la cadena de conexion lo hace
                if (ConexionValue == null)
                    ConexionValue = CrearConexion(CadenaConexionValue);

                // si no esta abierta aun la conexion, lo abre
                if (ConexionValue.State != ConnectionState.Open)
                    ConexionValue.Open();

                // retorna la conexion en modo interfaz, para que se adapte a cualquier implementacion de los distintos fabricantes de motores de bases de datos
                return ConexionValue;
            } // end get
        } // end Conexion

        #endregion

        #region "Lecturas"

        // Obtiene un DataSet a partir de un Procedimiento Almacenado.
        public DataSet TraerDataSet(string procedimientoAlmacenado)
        {
            var ObjDataSet = new DataSet();
            CrearDataAdapter(procedimientoAlmacenado).Fill(ObjDataSet);
            return ObjDataSet;
        } // end TraerDataset

        //Obtiene un DataSet a partir de un Procedimiento Almacenado y sus parámetros.
        public DataSet TraerDataSet(string procedimientoAlmacenado, params Object[] args)
        {
            var ObjDataSet = new DataSet();
            CrearDataAdapter(procedimientoAlmacenado, args).Fill(ObjDataSet);
            return ObjDataSet;
        } // end TraerDataset

        // Obtiene un DataSet a partir de un Query Sql.
        public DataSet TraerDataSetSql(string comandoSql)
        {
            var ObjDataSet = new DataSet();
            CrearDataAdapterSql(comandoSql).Fill(ObjDataSet);
            return ObjDataSet;
        } // end TraerDataSetSql

        // Obtiene un DataTable a partir de un Procedimiento Almacenado.
        public DataTable TraerDataTable(string procedimientoAlmacenado)
        {
            return TraerDataSet(procedimientoAlmacenado).Tables[0].Copy();
        } // end TraerDataTable

        //Obtiene un DataSet a partir de un Procedimiento Almacenado y sus parámetros.
        public DataTable TraerDataTable(string procedimientoAlmacenado, params Object[] args)
        {
            return TraerDataSet(procedimientoAlmacenado, args).Tables[0].Copy();
        } // end TraerDataTable

        //Obtiene un DataTable a partir de un Query SQL
        public DataTable TraerDataTableSql(string comandoSql)
        {
            return TraerDataSetSql(comandoSql).Tables[0].Copy();
        } // end TraerDataTableSql

        // Obtiene un DataReader a partir de un Procedimiento Almacenado.
        public IDataReader TraerDataReader(string procedimientoAlmacenado)
        {
            var ObjComando = Comando(procedimientoAlmacenado);
            return ObjComando.ExecuteReader();
        } // end TraerDataReader 

        // Obtiene un DataReader a partir de un Procedimiento Almacenado y sus parámetros.
        public IDataReader TraerDataReader(string procedimientoAlmacenado, params object[] args)
        {
            var ObjComando = Comando(procedimientoAlmacenado);
            CargarParametros(ObjComando, args);
            return ObjComando.ExecuteReader();
        } // end TraerDataReader

        // Obtiene un DataReader a partir de un Procedimiento Almacenado.
        public IDataReader TraerDataReaderSql(string comandoSql)
        {
            var ObjComando = ComandoSql(comandoSql);
            return ObjComando.ExecuteReader();
        } // end TraerDataReaderSql 

        // Obtiene un Valor Escalar a partir de un Procedimiento Almacenado. Solo funciona con SP's que tengan
        // definida variables de tipo output, para funciones escalares mas abajo se declara un metodo
        public object TraerValorOutput(string procedimientoAlmacenado)
        {
            // asignar el string sql al command
            var ObjComando = Comando(procedimientoAlmacenado);
            // ejecutar el command
            ObjComando.ExecuteNonQuery();
            // declarar variable de retorno
            Object resp = null;

            // recorrer los parametros del SP
            foreach (IDbDataParameter par in ObjComando.Parameters)
                // si tiene parametros de tipo IO/Output retornar ese valor
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    resp = par.Value;
            return resp;
        } // end TraerValor

        // Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros.
        public object TraerValorOutput(string procedimientoAlmacenado, params Object[] args)
        {
            // asignar el string sql al command
            var ObjComando = Comando(procedimientoAlmacenado);
            // cargar los parametros del SP
            CargarParametros(ObjComando, args);
            // ejecutar el command
            ObjComando.ExecuteNonQuery();
            // declarar variable de retorno
            Object resp = null;

            // recorrer los parametros del SP
            foreach (IDbDataParameter par in ObjComando.Parameters)
                // si tiene parametros de tipo IO/Output retornar ese valor
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    resp = par.Value;
            return resp;
        } // end TraerValor

        // Obtiene un Valor Escalar a partir de un Procedimiento Almacenado.
        public object TraerValorOutputSql(string comadoSql)
        {
            // asignar el string sql al command
            var ObjComando = ComandoSql(comadoSql);
            // ejecutar el command
            ObjComando.ExecuteNonQuery();
            // declarar variable de retorno
            Object resp = null;

            // recorrer los parametros del Query (uso tipico envio de varias sentencias sql en el mismo command)
            foreach (IDbDataParameter par in ObjComando.Parameters)
                // si tiene parametros de tipo IO/Output retornar ese valor
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    resp = par.Value;
            return resp;
        } // end TraerValor

        // Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado.
        public object TraerValorEscalar(string procedimientoAlmacenado)
        {
            var ObjComando = Comando(procedimientoAlmacenado);
            return ObjComando.ExecuteScalar();
        } // end TraerValorEscalar

        /// Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado, con Params de Entrada
        public Object TraerValorEscalar(string procedimientoAlmacenado, params object[] args)
        {
            var ObjComando = Comando(procedimientoAlmacenado);
            CargarParametros(ObjComando, args);
            return ObjComando.ExecuteScalar();
        } // end TraerValorEscalar

        // Obtiene un Valor de una funcion Escalar a partir de un Query SQL
        public object TraerValorEscalarSql(string comandoSql)
        {
            var ObjComando = ComandoSql(comandoSql);
            return ObjComando.ExecuteScalar();
        } // end TraerValorEscalarSql

        #endregion

        #region "Acciones"

        protected abstract IDbConnection CrearConexion(string cadena);
        protected abstract IDbCommand Comando(string procedimientoAlmacenado);
        protected abstract IDbCommand ComandoSql(string comandoSql);
        protected abstract IDataAdapter CrearDataAdapter(string procedimientoAlmacenado, params Object[] args);
        protected abstract IDataAdapter CrearDataAdapterSql(string comandoSql);
        protected abstract void CargarParametros(IDbCommand comando, Object[] args);
        //para cargar una imagen
        public byte[] TraerImagen(string procedimientoAlmacenado, params object[] args)
        {
            var ObjComando = Comando(procedimientoAlmacenado);
            CargarParametros(ObjComando, args);
            ObjComando.ExecuteScalar();
            byte[] imagen = (byte[])ObjComando.ExecuteScalar();

            return imagen;

        }
        // metodo sobrecargado para autenticarse contra el motor de BBDD

        public bool Autenticar()
        {
            if (Conexion.State != ConnectionState.Open)
                Conexion.Open();
            return true;
        }// end Autenticar

        // metodo sobrecargado para autenticarse contra el motor de BBDD
        public bool Autenticar(string vUsuario, string vPassword)
        {
            UsuarioValue = vUsuario;
            PasswordValue = vPassword;
            ConexionValue = CrearConexion(CadenaConexion);

            ConexionValue.Open();
            return true;
        }// end Autenticar

        public bool Autenticar(bool valor)
        {
            ConexionValue = CrearConexion(CadenaConexion);
            //ConexionValue = new SqlConnection(CadenaConexion);
            ConexionValue.Open();
            return true;
        }

        // cerrar conexion
        public void CerrarConexion()
        {
            if (Conexion.State != ConnectionState.Closed)
                ConexionValue.Close();
        }

        // end CerrarConexion

        // Ejecuta un Procedimiento Almacenado en la base. 
        public int Ejecutar(string procedimientoAlmacenado)
        {
            return Comando(procedimientoAlmacenado).ExecuteNonQuery();
        } // end Ejecutar

        // Ejecuta un query sql
        public int EjecutarSql(string comandoSql)
        {
            return ComandoSql(comandoSql).ExecuteNonQuery();
        } // end Ejecutar

        //Ejecuta un Procedimiento Almacenado en la base, utilizando los parámetros. 
        public int Ejecutar(string procedimientoAlmacenado, params Object[] args)
        {
            var ObjComando = Comando(procedimientoAlmacenado);
            CargarParametros(ObjComando, args);
            var resp = ObjComando.ExecuteNonQuery();
            for (var i = 0; i < ObjComando.Parameters.Count; i++)
            {
                var par = (IDbDataParameter)ObjComando.Parameters[i];
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    args.SetValue(par.Value, i - 1);
            }// end for
            return resp;
        } // end Ejecutar

        #endregion

        #region "Transacciones"

        protected IDbTransaction MTransaccion;
        protected bool EnTransaccion;

        //Comienza una Transacción en la base en uso. 
        public void IniciarTransaccion()
        {
            try
            {
                MTransaccion = Conexion.BeginTransaction();
                EnTransaccion = true;
            }// end try
            finally
            { EnTransaccion = false; }
        }// end IniciarTransaccion


        //Confirma la transacción activa. 
        public void TerminarTransaccion()
        {
            try
            { MTransaccion.Commit(); }
            finally
            {
                MTransaccion = null;
                EnTransaccion = false;
            }// end finally
        }// end TerminarTransaccion


        //Cancela la transacción activa.
        public void AbortarTransaccion()
        {
            try
            { MTransaccion.Rollback(); }
            finally
            {
                MTransaccion = null;
                EnTransaccion = false;
            }// end finally
        }// end AbortarTransaccion

        #endregion

    }
}
