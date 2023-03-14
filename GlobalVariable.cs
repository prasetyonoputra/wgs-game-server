using System;
using Wargaming.Core.GlobalParam;
using Wargaming.Core.GlobalParam.HelperSessionUser;
using Wargaming.Core.GlobalParam.HelperScenarioAktif;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wargaming.Core.GlobalParam
{
    // Server Config Data
    [Serializable]
    public class ServerConfig
    {
        public static int? ACTIVE_INDEX { get; set; }
        public static string SERVER_NAME { get; set; }
        public static string SERVICE_LOGIN { get; set; }
        public static string SERVICE_CB { get; set; }
        public static string SERVER_COLYSEUS { get; set; }

        public static int? OCEAN_BASE { get; set; }

        public ServerConfig(ServerConfigHelper config)
        {
            ACTIVE_INDEX = config.active_index;
            SERVER_NAME = config.server_name;
            SERVICE_LOGIN = config.service_login;
            SERVICE_CB = config.service_cb;
            SERVER_COLYSEUS = config.server_colyseus;
        }
    }

    // Active User Data
    [Serializable]
    public class SessionUser
    {
        public static string username { get; set; }
        public static long? id { get; set; }
        public static string name { get; set; }
        public static string jenis_user { get; set; }
        public static string bagian { get; set; }
        public static long? id_bagian { get; set; }
        public static long? id_kogas { get; set; }
        public static long? asisten { get; set; }
        public static string nama_asisten { get; set; }
        public static string jabatan { get; set; }

        public SessionUser(SessionUserHelper user)
        {
            username = user.username;
            id = user.id;
            name = user.name;

            if (user.jenis_user != null)
            {
                jenis_user = user.jenis_user.jenis_user;
            }

            if (user.bagian == null)
            {
                id_bagian = 0;
                bagian = "";
            }
            else
            {
                id_bagian = user.bagian.ID;
                bagian = user.bagian.nama_bagian;
            }

            if (user.asisten == null)
            {
                asisten = 0;
                nama_asisten = "";
            }
            else
            {
                asisten = user.asisten.ID;

                if (user.asisten.nama_asisten != null)
                {
                    nama_asisten = user.asisten.nama_asisten;
                }
                else
                {
                    nama_asisten = "";
                }
            }

            if (user.jabatan != null)
            {
                jabatan = user.jabatan.nama_jabatan;
            }
        }
    }

    // Skenario Active
    [Serializable]
    public class SkenarioAktif
    {
        public static long? ID_SKENARIO { get; set; }
        public static string NAMA_SKENARIO { get; set; }
        public static string ID_DOCUMENT { get; set; }
        public static DateTime HARI_H { get; set; }
        public static DateTime WaktuMulai { get; set; }
        public static DateTime WaktuAkhir { get; set; }

        public SkenarioAktif(SkenarioAktifHelper skenario)
        {
            ID_SKENARIO = skenario.ID;
            NAMA_SKENARIO = skenario.nama_skenario;
        }
    }

    // CB Terbaik (Sendiri / BLUE FORCE)
    public class CBSendiri
    {
        public static long? id_cb { get; set; }
        public static long? id_cb_eppkm { get; set; }
        public static long? id_kogas { get; set; }
        public static long? id_user { get; set; }
        public static string nama_kogas { get; set; }
        public static string tipe_cb { get; set; }
        public static string nama_document { get; set; }
        public static string hari { get; set; }

        public CBSendiri(CBTerbaikHelper cbTerbaik)
        {
            id_cb = cbTerbaik.id_cb;
            id_cb_eppkm = cbTerbaik.id_cb_eppkm;
            id_kogas = cbTerbaik.id_kogas;
            id_user = cbTerbaik.id_user;
            nama_kogas = cbTerbaik.nama_kogas;
            tipe_cb = cbTerbaik.tipe_cb;
            nama_document = cbTerbaik.nama_document;
            hari = cbTerbaik.hari;
        }
    }

    // CB Terbaik (Musuh / RED FORCE)
    public class CBMusuh
    {
        public static long? id_cb { get; set; }
        public static long? id_cb_eppkm { get; set; }
        public static long? id_kogas { get; set; }
        public static long? id_user { get; set; }
        public static string nama_kogas { get; set; }
        public static string tipe_cb { get; set; }
        public static string nama_document { get; set; }
        public static string hari { get; set; }

        public CBMusuh(CBTerbaikHelper cbTerbaik)
        {
            id_cb = cbTerbaik.id_cb;
            id_cb_eppkm = cbTerbaik.id_cb_eppkm;
            id_kogas = cbTerbaik.id_kogas;
            id_user = cbTerbaik.id_user;
            nama_kogas = cbTerbaik.nama_kogas;
            tipe_cb = cbTerbaik.tipe_cb;
            nama_document = cbTerbaik.nama_document;
            hari = cbTerbaik.hari;
        }
    }

    // List User
    [Serializable]
    public class PlayerData
    {
        public static List<PData> PLAYERS { get; set; }

        public PlayerData(SessionUserHelper user)
        {
            PLAYERS = new List<PData>();

            PLAYERS.Add(
                new PData
                {
                    username = user.username,
                    id = user.id,
                    name = user.name,
                    jenis_user = (user.jenis_user != null) ? user.jenis_user.jenis_user : null,
                    id_bagian = (user.bagian != null) ? user.bagian.ID : 0,
                    bagian = (user.bagian != null) ? user.bagian.nama_bagian : "",
                    asisten = (user.asisten != null) ? user.asisten.ID : 0,
                    nama_asisten = (user.asisten != null) ? user.asisten.nama_asisten : "",
                    jabatan = (user.jabatan != null) ? user.jabatan.nama_jabatan : "",
                    timeString = "0",
                }
            );
        }
    }

    public class PData
    {
        public string username;
        public long? id;
        public string name;
        public string jenis_user;
        public string bagian;
        public long? id_bagian;
        public long? id_kogas;
        public long? asisten;
        public string nama_asisten;
        public string jabatan;

        public string timeString;
        public bool isLoaded;
    }

    public class SceneLoad
    {
        public static string returnTo { get; set; }
    }
}