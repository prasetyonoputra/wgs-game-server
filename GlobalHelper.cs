using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

using UnityEngine;
using Wargaming.Core.GlobalParam.HelperDataAlutsista;

namespace Wargaming.Core.GlobalParam
{
    public static class GlobalHelper
    {
        [Serializable]
        public class MissionWalkerParam
        {
            public string time;
            public DateTime datetime;
            public Vector3 coordinate;
        }
    }

    public static class SpeedHelper
    {
        public enum speedType { kmph, mph, knot, mach }

        public static double GetKecepatanByType(string speed, string speed_type)
        {
            switch (speed_type)
            {
                case "km":
                    return double.Parse(speed);
                case "knot":
                    return double.Parse(speed) * 1.852;
                case "mach":
                    return double.Parse(speed) * 1192.68f;
                default:
                    return double.Parse(speed);
            }
        }

        public static speedType getSpeedType(string type)
        {
            switch (type)
            {
                case "km":
                    return speedType.kmph;
                case "m":
                    return speedType.mph;
                case "knot":
                    return speedType.knot;
                case "mach":
                    return speedType.mach;
                default:
                    return speedType.kmph;
            }
        }
    }

    public partial class ServerConfigHelper
    {
        public int? active_index { get; set; }
        public string server_name { get; set; }
        public string service_login { get; set; }
        public string service_cb { get; set; }
        public string server_colyseus { get; set; }
    }

    //[Serializable]
    //public class BundleConfig
    //{
    //    [XmlAttribute("name")]
    //    public string name { get; set; }
    //    [XmlElement("variant")]
    //    public string variant { get; set; }
    //}

    [Serializable]
    public class StartupConfigData
    {
        [XmlElement("DEFAULT_AIRCRAFT_ALTITUDE")]
        public int? DEFAULT_AIRCRAFT_ALTITUDE { get; set; }
        [XmlElement("DEFAULT_OCEAN_HEIGHT")]
        public int DEFAULT_OCEAN_HEIGHT { get; set; }
        [XmlElement("DEFAULT_AIRCRAFT_SCALE")]
        public float DEFAULT_AIRCRAFT_SCALE { get; set; }
        [XmlElement("DEFAULT_SHIP_SCALE")]
        public float DEFAULT_SHIP_SCALE { get; set; }
        [XmlElement("DEFAULT_VEHICLE_SCALE")]
        public float DEFAULT_VEHICLE_SCALE { get; set; }
        [XmlElement("TIME_DIFF")]
        public float TIME_DIFF { get; set; }
        [XmlElement("SERVER_TFG")]
        public string SERVER_TFG { get; set; }
        [XmlElement("SERVER_TFG_SINGLE")]
        public string SERVER_TFG_SINGLE { get; set; }
        [XmlElement("SERVER_WGS")]
        public string SERVER_WGS { get; set; }
    }

    [Serializable]
    public class ServiceData
    {
        [XmlAttribute("name")]
        public string ServiceName { get; set; }
        [XmlElement("ServiceLogin")]
        public string ServiceLogin { get; set; }
        [XmlElement("ServiceCB")]
        public string ServiceCB { get; set; }
        [XmlElement("ServerColyseus")]
        public string ServerColyseus { get; set; }
        [XmlElement("Active")]
        public bool Active { get; set; }
    }


    namespace HelperSessionUser
    {
        public partial class SessionUserHelper
        {
            public string username { get; set; }
            public long? id { get; set; }
            public string name { get; set; }
            public JenisUser jenis_user { get; set; }
            public Bagian bagian { get; set; }
            public Jabatan jabatan { get; set; }
            public Asisten asisten { get; set; }
            public long? atasan { get; set; }
            public long? status_login { get; set; }

            public static SessionUserHelper[] FromJson(string json) => JsonConvert.DeserializeObject<SessionUserHelper[]>(json, HelperConverter.Converter.Settings);
        }

        public partial class JenisUser
        {
            public long ID { get; set; }
            public string jenis_user { get; set; }
        }

        public partial class Bagian
        {
            public long ID { get; set; }
            public string nama_bagian { get; set; }
        }

        public partial class Jabatan
        {
            public long ID { get; set; }
            public string nama_jabatan { get; set; }
        }

        public partial class Asisten
        {
            public long ID { get; set; }
            public string nama_asisten { get; set; }
        }
    }

    namespace HelperScenarioAktif
    {
        public partial class SkenarioAktifHelper
        {
            public long? ID { get; set; }
            public string nama_skenario { get; set; }

            public static SkenarioAktifHelper FromJson(string json) => JsonConvert.DeserializeObject<SkenarioAktifHelper>(json, HelperConverter.Converter.Settings);
        }

        public partial class SkenarioAktifWaktu
        {
            public string ID { get; set; }
            public string create_by { get; set; }
            public string harih { get; set; }
            public string harih_kogab { get; set; }
            public string hide { get; set; }
            public string jenis_operasi { get; set; }
            public string kode_skenario_surat { get; set; }
            public string layer_pilihan { get; set; }
            public string nama_skenario { get; set; }
            public string nomor_skenario_surat { get; set; }
            public string pasukan_check { get; set; }
            public string perbandingan { get; set; }
            public string perencanaan { get; set; }
            public string pilihan_cb_terbaik { get; set; }
            public string posisi_x { get; set; }
            public string posisi_y { get; set; }
            public string service { get; set; }
            public string status { get; set; }
            public string tgl_modifikasi { get; set; }
            public string tgl_mulai_asum { get; set; }
            public string tgl_mulai_asum_kogab { get; set; }
            public string tgl_mulai_asum_kogas { get; set; }
            public string tgl_mulai_real { get; set; }
            public string tgl_mulai_real_kogab { get; set; }
            public string tgl_pembuatan { get; set; }
            public string tgl_selesai_asum { get; set; }
            public string tgl_mulai_real_kogas { get; set; }
            public string tgl_selesai_asum_kogab { get; set; }
            public string tgl_selesai_asum_kogas { get; set; }
            public string tgl_selesai_real { get; set; }
            public string tgl_selesai_real_kogab { get; set; }
            public string tgl_selesai_real_kogas { get; set; }
            public string tipe_skenario { get; set; }
            public string zoom { get; set; }

            public static SkenarioAktifWaktu FromJson(string json) => JsonConvert.DeserializeObject<SkenarioAktifWaktu>(json, HelperConverter.Converter.Settings);
        }

        public partial class CBTerbaikHelper
        {
            public long? id_cb { get; set; }
            public long? id_cb_eppkm { get; set; }
            public long? id_kogas { get; set; }
            public long? id_user { get; set; }
            public string nama_kogas { get; set; }
            public string tipe_cb { get; set; }
            public int? id_document { get; set; }
            public string nama_document { get; set; }
            public string hari { get; set; }

            public static CBTerbaikHelper[] FromJson(string json) => JsonConvert.DeserializeObject<CBTerbaikHelper[]>(json, HelperConverter.Converter.Settings);
        }

        public partial class DocumentActiveHelper
        {
            public string id { get; set; }
            public static DocumentActiveHelper FromJson(string json) => JsonConvert.DeserializeObject<DocumentActiveHelper>(json, HelperConverter.Converter.Settings);
        }
    }

    namespace HelperKegiatan
    {
        public partial class TimetableKegiatanHelper
        {
            public string id_kegiatan { get; set; }
            public string id_satuan { get; set; }
            public string id_user { get; set; }
            public string kegiatan { get; set; }
            public string keterangan { get; set; }
            //public DateTime waktu { get; set; }
            public string waktu { get; set; }
            public DateTime waktuDT { get; set; }
            //public DateTime waktu { get; set; }   
            public string x { get; set; }
            public string y { get; set; }
            public string kogas { get; set; }
            public string waypoint { get; set; }
            public string detail { get; set; }
            public string nama_object { get; set; }
            public string type_kegiatan { get; set; }
            public string url_video { get; set; }
            public string percepatan { get; set; }

            public static TimetableKegiatanHelper[] FromJson(string json) => JsonConvert.DeserializeObject<TimetableKegiatanHelper[]>(json, HelperConverter.Converter.Settings);
        }
    }

    namespace HelperColyseusTFG
    {
        public partial class FocusToObject
        {
            public string id { get; set; }
            public string id_satuan { get; set; }
            public string id_video { get; set; }
        }

        public partial class JumpDateObj
        {
            public string changeTime { get; set; }
        }

        public partial class ResyncPosition
        {
            public string id_satuan { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }
            public double heading { get; set; }
        }

        public partial class ZoomToObject
        {
            public string id { get; set; }
        }
    }

    namespace HelperPlotting
    {
        #region Entity Satuan
        public partial class EntitySatuan
        {
            [JsonProperty("id")]
            public string id_entity { get; set; }
            public string id_user { get; set; }
            public string id_symbol { get; set; }
            public string dokumen { get; set; }
            [JsonProperty("nama")]
            public string id_satuan { get; set; }
            [JsonProperty("lat_y")]
            public float lat { get; set; }
            [JsonProperty("lng_x")]
            public float lng { get; set; }
            [JsonProperty("style")]
            private string _styleString { set { data_style = EntitySatuanStyle.FromJson(value); } }
            public EntitySatuanStyle data_style { get; set; }
            [JsonProperty("info")]
            private string _infoString { set { data_info = EntitySatuanInfo.FromJson(value); } }
            public EntitySatuanInfo data_info { get; set; }
            public string id_kegiatan { get; set; }
            public string isi_logistik { get; set; }
            public float heading { get; set; }
            public float height { get; set; }
            public Detector detector { get; set; }

            // CUSTOM DATA
            public string tipe_tni { get; set; }
            public DataSatuan.JenisSatuan jenis { get; set; }
            public string path_object_3d { get; set; }
            public string object3D { get; set; }
            public string alutsista { get; set; }
            public HelperDataAlutsista.RadarSatuan[] radar { get; set; }
            public HelperDataAlutsista.SonarSatuan[] sonar { get; set; }
            public Dictionary<string, object> allSenjata { get; set; }

            public static string ToString(EntitySatuan json) => JsonConvert.SerializeObject(json);
            public static EntitySatuan FromJson(JToken json) => JsonConvert.DeserializeObject<EntitySatuan>(json.ToString(), HelperConverter.Converter.Settings);
        }

        public partial class EntitySatuanStyle
        {
            [JsonProperty("nama")]
            public string font_taktis { get; set; }
            [JsonProperty("index")]
            public int? font_ascii { get; set; }
            public string grup { get; set; }
            public string label_style { get; set; }
            public string keterangan { get; set; }

            public static string ToString(EntitySatuanStyle json) => JsonConvert.SerializeObject(json);
            public static EntitySatuanStyle FromJson(string json) => JsonConvert.DeserializeObject<EntitySatuanStyle>(json, HelperConverter.Converter.Settings);
        }

        public partial class EntitySatuanInfo
        {
            [JsonProperty("kecepatan")]
            private string kecepatanString { set { jenis_kecepatan = (value.Split("|").Length > 1) ? value.Split("|")[1] : null; kecepatan_satuan = (value.Split("|").Length >= 1) ? float.Parse(value.Split("|")[0]) : float.Parse(value); } }
            public float? kecepatan_satuan { get; set; }
            public string jenis_kecepatan { get; set; }
            public string nomer_satuan { get; set; }
            public string nama_satuan { get; set; }
            public string nomer_atasan { get; set; }
            public string tgl_mulai { get; set; }
            public string tgl_selesai { get; set; }
            public string warna { get; set; }
            public string size { get; set; }
            public string weapon { get; set; }
            //public List<object> waypoint { get; set; }
            //public List<object> list_embarkasi { get; set; }
            public float armor { get; set; }
            public bool id_dislokasi { get; set; }
            public bool id_dislokasi_obj { get; set; }
            public string bahan_bakar { get; set; }
            public string bahan_bakar_load { get; set; }
            public string kecepatan_maks { get; set; }
            public string heading { get; set; }
            public string ket_satuan { get; set; }
            public string nama_icon_satuan { get; set; }
            public string width_icon_satuan { get; set; }
            public string height_icon_satuan { get; set; }
            public int? personil { get; set; }
            public bool radarList { get; set; }
            public bool sonarList { get; set; }
            public bool tfg { get; set; }
            public string ketinggian { get; set; }
            public bool hidewgs { get; set; }

            public static EntitySatuanInfo FromJson(string json) => JsonConvert.DeserializeObject<EntitySatuanInfo>(json, HelperConverter.Converter.Settings);
            public static string ToString(EntitySatuanInfo json) => JsonConvert.SerializeObject(json);
        }

        #endregion

        #region ENTITY_OBSTACLE
        public partial class EntityObstacle
        {
            public string dokumen { get; set; }
            public string id { get; set; }
            public string id_user { get; set; }
            public string info_obstacle { set { infoObstacle = InfoObstacle.FromJson(value); } }
            public InfoObstacle infoObstacle { get; set; }
            [JsonProperty("lat_y")]
            public float lat { get; set; }
            [JsonProperty("lng_x")]
            public float lng { get; set; }
            public string nama { get; set; }
            public string symbol { get; set; }

            public static string ToString(EntityObstacle json) => JsonConvert.SerializeObject(json);
            public static EntityObstacle FromJson(string json) => JsonConvert.DeserializeObject<EntityObstacle>(json, HelperConverter.Converter.Settings);
        }

        public partial class InfoObstacle
        {
            public string nama { get; set; }
            public string font { get; set; }
            public string warna { get; set; }
            public int size { get; set; }
            public string info { get; set; }
            public string index { get; set; }
            public int id_user { get; set; }

            public static string ToString(InfoObstacle json) => JsonConvert.SerializeObject(json);
            public static InfoObstacle FromJson(string json) => JsonConvert.DeserializeObject<InfoObstacle>(json, HelperConverter.Converter.Settings);
        }
        #endregion

        #region ENTITY_FORMASI
        public partial class EntityFormasi
        {
            public string id { get; set; }
            public string id_user { get; set; }
            public string dokumen { get; set; }
            public string nama { get; set; }
            public string info_formasi { set { infoFormasi = InfoFormasi.FromJson(value); } }
            public InfoFormasi infoFormasi { get; set; }
            [JsonProperty("lat_y")]
            public float? lat { get; set; }
            [JsonProperty("lng_x")]
            public float? lng { get; set; }
            public string tgl_formasi { get; set; }
            public string kecepatan { get; set; }
            public string size { get; set; }
            public string jenis_formasi { get; set; }
            public string symbol { get; set; }
            public string warna { get; set; }

            public static string ToString(EntityFormasi json) => JsonConvert.SerializeObject(json);
            public static EntityFormasi FromJson(string json) => JsonConvert.DeserializeObject<EntityFormasi>(json, HelperConverter.Converter.Settings);
        }

        public partial class IconSatuanFormasi
        {
            public string nama { get; set; }
            public string index { get; set; }
            public string keterangan { get; set; }
            public string grup { get; set; }
            public string entity_name { get; set; }
            public string entity_kategori { get; set; }
            public string jenis_role { get; set; }
            public string label_style { get; set; }
        }


        public partial class SatuanFormasi
        {
            public string id_point { get; set; }
            public string nama { get; set; }
            public IconSatuanFormasi icon { get; set; }
            public string color { get; set; }
            public float lat { get; set; }
            public float lng { get; set; }
            public string inti { get; set; }
            public string id { get; set; }
            public string jarak { get; set; }
            public string new_jarak { get; set; }
            public string sudut { get; set; }
            public string satuan { get; set; }
            public static string ToString(SatuanFormasi json) => JsonConvert.SerializeObject(json);
            public static SatuanFormasi FromJson(string json) => JsonConvert.DeserializeObject<SatuanFormasi>(json, HelperConverter.Converter.Settings);

        }

        public partial class InfoFormasi
        {
            public string nama_formasi { get; set; }
            public string warna { get; set; }
            public string size { get; set; }
            public string id_point { get; set; }
            public int arah { get; set; }
            public int[] arrArah { get; set; }
            public SatuanFormasi[] satuan_formasi { get; set; }

            public static string ToString(InfoFormasi json) => JsonConvert.SerializeObject(json);
            public static InfoFormasi FromJson(string json) => JsonConvert.DeserializeObject<InfoFormasi>(json, HelperConverter.Converter.Settings);
        }
        #endregion

        #region ENTITY_RADAR
        public partial class EntityRadar
        {
            public string id { get; set; }
            public string id_user { get; set; }
            public string dokumen { get; set; }
            public string nama { get; set; }
            [JsonProperty("lat_y")]
            public float lat { get; set; }
            [JsonProperty("lng_x")]
            public float lng { get; set; }
            public string info_radar { get; set; }
            public string symbol { get; set; }
            public string info_symbol { get; set; }

            public static string ToString(EntityRadar json) => JsonConvert.SerializeObject(json);
            public static EntityRadar FromJson(string json) => JsonConvert.DeserializeObject<EntityRadar>(json, HelperConverter.Converter.Settings);
        }

        public partial class EntityRadarInfo
        {
            public string nama { get; set; }
            public string judul { get; set; }
            public float radius { get; set; }
            public int size { get; set; }
            public string warna { get; set; }
            public EntityRadarInfoSymbol id_symbol { get; set; }
            public string jenis_radar { get; set; }

            public static EntityRadarInfo FromJson(string json) => JsonConvert.DeserializeObject<EntityRadarInfo>(json, HelperConverter.Converter.Settings);
        }

        public partial class EntityRadarInfoSymbol
        {
            public string id { get; set; }
            public string nama { get; set; }
            public string index { get; set; }
            public string keterangan { get; set; }
            public string grup { get; set; }
        }
        #endregion

        #region Misi Satuan
        public partial class MisiSatuan
        {
            public string id { get; set; }
            public string id_mission { get; set; }
            public string testID { get; set; }
            public string id_object { get; set; }
            public string tgl_mulai { get; set; }
            public string id_user { get; set; }
            public string missionDefault { get; set; }
            public string jenis { get; set; }
            [JsonProperty("properties")]
            private string _propertiesString { set { data_properties = MisiSatuanProperties.FromJson(value); } }
            public MisiSatuanProperties data_properties { get; set; }

            public static string getString(MisiSatuanProperties json) => JsonConvert.SerializeObject(json);
            public static MisiSatuan FromJson(string json) => JsonConvert.DeserializeObject<MisiSatuan>(json, HelperConverter.Converter.Settings);
        }

        public partial class MisiSatuanProperties
        {
            public string nama_misi { get; set; }
            public List<JalurMisi> jalur { get; set; }
            public List<JalurMisi> koordTujuan { get; set; }
            public JalurMisi koordLifeboat { get; set; }
            public List<string> listEmbarkasi { get; set; }
            public string kecepatan { get; set; }
            [JsonProperty("idTujuan")]
            public string id_tujuan { get; set; }
            public string idDebarkasi { get; set; }
            public string type { get; set; }
            public double? heading { get; set; }
            public double? distance { get; set; }
            public string typeMisi { get; set; }
            public string id_kegiatan { get; set; }
            public string idPrimary { get; set; }
            public string jenis { get; set; }
            public string objek { get; set; }
            public string misiEmDeb { get; set; }
            public string tools { get; set; }

            public static MisiSatuanProperties FromJson(string json) => JsonConvert.DeserializeObject<MisiSatuanProperties>(json, HelperConverter.Converter.Settings);
        }

        public partial class JalurMisi
        {
            public float lat { get; set; }
            public float lng { get; set; }
            public int? alt { get; set; }
        }
        #endregion
    }

    namespace HelperDataAlutsista
    {

        #region Senjata
        public class RadarSatuan
        {
            [JsonProperty("RADAR_ID")]
            public int RADAR_ID { get; set; }

            [JsonProperty("RADAR_NAME")]
            public string RADAR_NAME { get; set; }
            public string DESCRIPTION { get; set; }
            public string MATRA { get; set; }
            public string RADAR_AZIMUTH { get; set; }
            public string RADAR_DET_RANGE { get; set; }
            public string RADAR_JAMM_RANGE { get; set; }
            public string RADAR_MAX_RANGE { get; set; }
            public string RADAR_SYM_ID { get; set; }
            public string RADAR_TYPE { get; set; }
            public string TYPE_DESC { get; set; }
            public string negara_pembuat { get; set; }
            public string perusahaan_pembuat { get; set; }
            public string tahun_pembuat { get; set; }
            public int used { get; set; }

            public static RadarSatuan FromJson(string json) => JsonConvert.DeserializeObject<RadarSatuan>(json, HelperConverter.Converter.Settings);
        }

        public class SonarSatuan
        {
            public string DATATYPE { get; set; }
            public string DESCRIPTION { get; set; }
            public string SONAR_ACC_SRC_LEV { get; set; }
            public string SONAR_ACTIVE_50_DET_RNG { get; set; }
            public string SONAR_ACTIVE_100_DET_RG { get; set; }
            public string SONAR_ACTIVE_ABSORB_COEFF { get; set; }
            public string SONAR_ACTIVE_ATTEN_R50 { get; set; }
            public string SONAR_ACTIVE_ATTEN_R100 { get; set; }
            public string SONAR_ACTIVE_FREQ_OF_OPS { get; set; }
            public string SONAR_ACT_CS { get; set; }
            public string SONAR_ACT_DET_RG { get; set; }
            public string SONAR_ACT_RG_100 { get; set; }
            public string SONAR_ARRAY_KINKS { get; set; }
            public string SONAR_ARRAY_LENGTH { get; set; }
            public string SONAR_ASSOC_ACC_CS { get; set; }
            public string SONAR_ASSOC_ACC_SL { get; set; }
            public string SONAR_AVERAGE_BEAMWIDTH { get; set; }
            public string SONAR_BEAM_WIDTH { get; set; }
            public string SONAR_BEARING_AMBIGUITY { get; set; }
            public string SONAR_CABLE_LENGTH { get; set; }
            public string SONAR_CABLE_PAYOUT { get; set; }
            public string SONAR_CATEGORY { get; set; }
            public string SONAR_DC_MINE { get; set; }
            public string SONAR_DC_SUB { get; set; }
            public string SONAR_DC_SURF { get; set; }
            public string SONAR_DC_TORP { get; set; }
            public string SONAR_DEPTH_FINDING { get; set; }
            public string SONAR_ID { get; set; }
            public string SONAR_INT_PER_ACTIVE { get; set; }
            public string SONAR_INT_PER_PASIVE { get; set; }
            public string SONAR_MAX_BEARING_ERROR { get; set; }
            public string SONAR_MAX_DEPTH { get; set; }
            public string SONAR_MAX_DETECT_RANGE { get; set; }
            public string SONAR_MAX_SONAR_SPEED { get; set; }
            public string SONAR_MAX_SPEED { get; set; }
            public string SONAR_MIN_DEPTH { get; set; }
            public string SONAR_MIN_DEPTH_TO_DEPLOY { get; set; }
            public string SONAR_MIN_SPEED { get; set; }
            public string SONAR_MIN_TOW_SPEED { get; set; }
            public string SONAR_NAME { get; set; }
            public string SONAR_OP_FREQ_ACT { get; set; }
            public string SONAR_MIN_DSONAR_OP_FREQ_PASVEPTH { get; set; }
            public string SONAR_OP_MODE { get; set; }
            public string SONAR_OP_POWER { get; set; }
            public string SONAR_OWNSHIP_NOISE_INC { get; set; }
            public string SONAR_PASIVE_50_DET_RNG { get; set; }
            public string SONAR_PASIVE_100_DET_RG { get; set; }
            public string SONAR_PASIVE_ABSORB_COEFF { get; set; }
            public string SONAR_PASIVE_ATTEN_R50 { get; set; }
            public string SONAR_PASIVE_ATTEN_R100 { get; set; }
            public string SONAR_PASIVE_FREQ_OF_OPS { get; set; }
            public string SONAR_PASV_DET_RG { get; set; }
            public string SONAR_PASV_RG_100 { get; set; }
            public string SONAR_SOURCE_LEVEL { get; set; }
            public string SONAR_TGT_ID_CAP { get; set; }
            public string SONAR_TIME_TO_ID_TGT { get; set; }
            public string SONAR_TIME_TO_UNKINK { get; set; }
            public string SONAR_TMA_CAPABLE { get; set; }
            public string SONAR_TM_INTV_OF_WTCH_LONG { get; set; }
            public string SONAR_TM_INTV_OF_WTCH_MED { get; set; }
            public string SONAR_TM_INTV_OF_WTCH_SHORT { get; set; }
            public string SONAR_TRACKING_CAPABLE { get; set; }
            public string SONAR_TURN_RATE_TO_KINKS { get; set; }
            public string TYPE_DESC { get; set; }
            public string negara_pembuat { get; set; }
            public string tahun_pembuat { get; set; }

            public static SonarSatuan FromJson(string json) => JsonConvert.DeserializeObject<SonarSatuan>(json, HelperConverter.Converter.Settings);
        }

        public class TorpedoWeapon
        {
            public string AIRCRAFT_ID { get; set; }
            public string SHIP_ID { get; set; }
            public string COUNTRY_ID { get; set; }
            public string DAMAGE_POWER { get; set; }
            public string DATATYPE { get; set; }
            public string DESCRIPTION { get; set; }
            public string DETECT_TYPE { get; set; }
            public string ID_WEAPON { get; set; }
            public string LAUNCH_TYPE { get; set; }
            public string TARGET_DOMAIN { get; set; }
            public string TORPEDO_ACCL_RATE { get; set; }
            public string TORPEDO_ACI_ACAV_SPD { get; set; }
            public string TORPEDO_ACI_BCAV_SPD { get; set; }
            public string TORPEDO_ACI_MAX_SPD { get; set; }
            public string TORPEDO_ACI_MIN_SPD { get; set; }
            public string TORPEDO_ACS_FRONT { get; set; }
            public string TORPEDO_ACS_SIDE { get; set; }
            public string TORPEDO_ACTIVE_SEEKER_FREQ { get; set; }
            public string TORPEDO_ACTIVE_SEEKER_POWER { get; set; }
            public string TORPEDO_ADPH_FRONT { get; set; }
            public string TORPEDO_ADPH_FRONT_SIDE { get; set; }
            public string TORPEDO_ADPH_REAR { get; set; }
            public string TORPEDO_ADPH_REAR_SIDE { get; set; }
            public string TORPEDO_AFT { get; set; }
            public string TORPEDO_AFT_QTR { get; set; }
            public string TORPEDO_AIR_DROP_CAP { get; set; }
            public string TORPEDO_ANTI_SUB { get; set; }
            public string TORPEDO_ANTI_SURF { get; set; }
            public string TORPEDO_BASELINE_PROP_HIT { get; set; }
            public string TORPEDO_CAV_SPEED { get; set; }
            public string TORPEDO_CIRCLE_OP_MOD { get; set; }
            public string TORPEDO_COUNT { get; set; }
            public string TORPEDO_CRUISE_SPEED { get; set; }
            public string TORPEDO_DAMAGE { get; set; }
            public string TORPEDO_DAMAGE_SUSTAIN { get; set; }
            public string TORPEDO_DCCL_RATE { get; set; }
            public string TORPEDO_DEF_CRUISE_DEPTH { get; set; }
            public string TORPEDO_DEPTH { get; set; }
            public string TORPEDO_DESCENT_RATE { get; set; }
            public string TORPEDO_DETECT_TYPE { get; set; }
            public string TORPEDO_DIAMETERS { get; set; }
            public string TORPEDO_ENDURANCE_TYPE { get; set; }
            public string TORPEDO_ENGAGE_RANGE { get; set; }
            public string TORPEDO_FIRST_GYRO_ANGLE { get; set; }
            public string TORPEDO_FIX_DEPTH { get; set; }
            public string TORPEDO_FWD { get; set; }
            public string TORPEDO_FWD_QTR { get; set; }
            public string TORPEDO_HELM_ANG_RATE { get; set; }
            public string TORPEDO_HH { get; set; }
            public string TORPEDO_HIGH_SPEED { get; set; }
            public string TORPEDO_HIGH_SPEED_ENDUR { get; set; }
            public string TORPEDO_ID { get; set; }
            public string TORPEDO_LATERAL_DECEL { get; set; }
            public string TORPEDO_LAUNCH_METHOD { get; set; }
            public string TORPEDO_LAUNCH_SPEED { get; set; }
            public string TORPEDO_LENGTH { get; set; }
            public string TORPEDO_LETHALITY { get; set; }
            public string TORPEDO_MAX_ALTITUDE { get; set; }
            public string TORPEDO_MAX_CLIMB { get; set; }
            public string TORPEDO_MAX_DEPTH { get; set; }
            public string TORPEDO_MAX_DESCENT { get; set; }
            public string TORPEDO_MAX_EFF_RANGE { get; set; }
            public string TORPEDO_MAX_HELP_ANG { get; set; }
            public string TORPEDO_MAX_RANGE { get; set; }
            public string TORPEDO_MAX_SEARCH_DEPTH { get; set; }
            public string TORPEDO_MAX_SPEED { get; set; }
            public string TORPEDO_MAX_SPEED_ENDUR { get; set; }
            public string TORPEDO_MIN_RANGE { get; set; }
            public string TORPEDO_MIN_RUN_OUT_RANGE { get; set; }
            public string TORPEDO_MIN_SPEED { get; set; }
            public string TORPEDO_MM { get; set; }
            public string TORPEDO_NAME { get; set; }
            public string TORPEDO_NOISE_RATING { get; set; }
            public string TORPEDO_NORM_CLIMB { get; set; }
            public string TORPEDO_NORM_DESCENT { get; set; }
            public string TORPEDO_OLR_CONV_15 { get; set; }
            public string TORPEDO_OLR_CONV_20 { get; set; }
            public string TORPEDO_OLR_NUC { get; set; }
            public string TORPEDO_OP_MODIFY { get; set; }
            public string TORPEDO_PHM_ACT_ACC { get; set; }
            public string TORPEDO_PHM_ACT_PASS { get; set; }
            public string TORPEDO_PHM_PASS_ACC { get; set; }
            public string TORPEDO_PHM_WIRE_GUIDE { get; set; }
            public string TORPEDO_PHM_WK_HOMING { get; set; }
            public string TORPEDO_PRIMARY_GUIDE_TYPE { get; set; }
            public string TORPEDO_PRI_TARGET_DOMAIN { get; set; }
            public string TORPEDO_PROB_OF_HIT { get; set; }
            public string TORPEDO_PURSUIT_TYPE { get; set; }
            public string TORPEDO_SAFETY_CEILING_DEPTH { get; set; }
            public string TORPEDO_SECOND_GYRO_ANGLE { get; set; }
            public string TORPEDO_SEEKER_AZIMUTH { get; set; }
            public string TORPEDO_SEEKER_ELEVATION { get; set; }
            public string TORPEDO_SEEKER_RANGE { get; set; }
            public string TORPEDO_SEEKER_TURN_ON_RNG { get; set; }
            public string TORPEDO_SIN_AMPLITUDE { get; set; }
            public string TORPEDO_SIN_PERIOD { get; set; }
            public string TORPEDO_SIN_RUN_OUT { get; set; }
            public string TORPEDO_SS { get; set; }
            public string TORPEDO_STD_TURN_RATE { get; set; }
            public string TORPEDO_TERM_CIRCLE_RAD { get; set; }
            public string TORPEDO_TIGHT_TURN_RATE { get; set; }
            public string TORPEDO_USE_GYRO_ANGLE { get; set; }
            public string TORPEDO_USE_TERM_CIRCLE { get; set; }
            public string TORPEDO_VERT_ACCELL { get; set; }
            public string TORPEDO_WARHEAD_WEIGHT { get; set; }
            public string TORPEDO_WEAPON_GENERATION { get; set; }
            public string TORPEDO_WEIGHT { get; set; }
            public string TORPEDO_WIDTH_HEIGTH { get; set; }
            public string TORPEDO_WIRE_ANGLE_OFFS { get; set; }
            public string TYPE_DESC { get; set; }
            public string jarak_tembak { get; set; }
            public string jarak_tembak_min { get; set; }
            public int jumlah_peluru { get; set; }
            public string kecepatan_awal { get; set; }
            public string name { get; set; }
            public string negara_pembuat { get; set; }
            public string perusahaan_pembuat { get; set; }
            public string radius_damage { get; set; }
            public string tahun_pembuat { get; set; }
        }

        public class MissileWeapon
        {
            public string AIRCRAFT_ID { get; set; }
            public string SHIP_ID { get; set; }
            public string jumlah_peluru { get; set; }
            public string COUNTRY_ID { get; set; }
            public string DAMAGE_POWER { get; set; }
            public string DATATYPE { get; set; }
            public string DESCRIPTION { get; set; }
            public string DETECT_TYPE { get; set; }
            public string ID_WEAPON { get; set; }
            public string LAUNCH_TYPE { get; set; }
            public string MISSILE_ACCEL_RATE { get; set; }
            public string MISSILE_ARH_BAND_LOWER { get; set; }
            public string MISSILE_ARH_BAND_UPPER { get; set; }
            public string MISSILE_AZIMUTH { get; set; }
            public string MISSILE_BB_BOX_HEIGHT { get; set; }
            public string MISSILE_BB_BOX_RANGE { get; set; }
            public string MISSILE_BB_BOX_WIDTH { get; set; }
            public string MISSILE_BOOST_DURATION { get; set; }
            public string MISSILE_COUNT { get; set; }
            public string MISSILE_CRUISE_ALT { get; set; }
            public string MISSILE_CRUISE_SPD { get; set; }
            public string MISSILE_DAMAGE { get; set; }
            public string MISSILE_DAMAGE_SUSTAIN { get; set; }
            public string MISSILE_DECEL_RATE { get; set; }
            public string MISSILE_DEF_CRUISE_ALT { get; set; }
            public string MISSILE_DIAMETER { get; set; }
            public string MISSILE_ECCM_TYPE { get; set; }
            public string MISSILE_ENDUR_TYPE { get; set; }
            public string MISSILE_ENGAGE_RAD { get; set; }
            public string MISSILE_HAFO_RANGE { get; set; }
            public string MISSILE_HAFO_REQD { get; set; }
            public string MISSILE_HAVO_ALT { get; set; }
            public string MISSILE_HELM_ANG_RATE { get; set; }
            public string MISSILE_HIGH_SPD { get; set; }
            public string MISSILE_HOME_ON_JAMMER_A { get; set; }
            public string MISSILE_HOME_ON_JAMMER_B { get; set; }
            public string MISSILE_HOME_ON_JAMMER_C { get; set; }
            public string MISSILE_ID { get; set; }
            public string MISSILE_INITIAL_SPEED { get; set; }
            public string MISSILE_IRCM_DETECT { get; set; }
            public string MISSILE_IRCM_DETONATE { get; set; }
            public string MISSILE_IRCS_FRONT { get; set; }
            public string MISSILE_IRCS_SIDE { get; set; }
            public string MISSILE_LENGTH { get; set; }
            public string MISSILE_LETHALITY { get; set; }
            public string MISSILE_MAX_ALT { get; set; }
            public string MISSILE_MAX_CLIMB { get; set; }
            public string MISSILE_MAX_DEPTH { get; set; }
            public string MISSILE_MAX_DESCENT { get; set; }
            public string MISSILE_MAX_EFF_RANGE { get; set; }
            public string MISSILE_MAX_FIR_DEPTH { get; set; }
            public string MISSILE_MAX_G { get; set; }
            public string MISSILE_MAX_HELM_ANG { get; set; }
            public string MISSILE_MAX_RANGE { get; set; }
            public string MISSILE_MAX_SPD_KNOT { get; set; }
            public string MISSILE_MAX_SPD_MATCH { get; set; }
            public string MISSILE_MID_COURSE_TYPE { get; set; }
            public string MISSILE_MIN_ALT { get; set; }
            public string MISSILE_MIN_RANGE { get; set; }
            public string MISSILE_MIN_SPEED { get; set; }
            public string MISSILE_MISS_GEN { get; set; }
            public string MISSILE_NAME { get; set; }
            public string MISSILE_NORM_CLIMB { get; set; }
            public string MISSILE_NORM_DESCENT { get; set; }
            public string MISSILE_PAGE_REV { get; set; }
            public string MISSILE_POH { get; set; }
            public string MISSILE_POHM_ARH { get; set; }
            public string MISSILE_POHM_IRH { get; set; }
            public string MISSILE_POHM_SARH { get; set; }
            public string MISSILE_POHM_TARH { get; set; }
            public string MISSILE_PRIMARY_GUIDE { get; set; }
            public string MISSILE_PROB_OF_HIT { get; set; }
            public string MISSILE_PULS_WID_US { get; set; }
            public string MISSILE_PURSUIT_TYPE { get; set; }
            public string MISSILE_RANGE { get; set; }
            public string MISSILE_RCS { get; set; }
            public string MISSILE_RDR_CS_FRONT { get; set; }
            public string MISSILE_RDR_CS_SIDE { get; set; }
            public string MISSILE_SCAN_RATE { get; set; }
            public string MISSILE_SEA_SKIMMER { get; set; }
            public string MISSILE_SEA_STATE_MODEL { get; set; }
            public string MISSILE_SECOND_GUIDE { get; set; }
            public string MISSILE_SKENV_AZIMUTH { get; set; }
            public string MISSILE_SKENV_ELEVATION { get; set; }
            public string MISSILE_SKENV_RANGE { get; set; }
            public string MISSILE_SPOT_NUMBER { get; set; }
            public string MISSILE_STD_TURN_RATE { get; set; }
            public string MISSILE_STO_OPMOD { get; set; }
            public string MISSILE_STO_RANGE { get; set; }
            public string MISSILE_TARGET_TYPE_AIR { get; set; }
            public string MISSILE_TARGET_TYPE_LAND { get; set; }
            public string MISSILE_TARGET_TYPE_SUB { get; set; }
            public string MISSILE_TARGET_TYPE_SURF { get; set; }
            public string MISSILE_TARH_ECHR_7100 { get; set; }
            public string MISSILE_TARH_ECHR_7775 { get; set; }
            public string MISSILE_TA_ALTREQ { get; set; }
            public string MISSILE_TA_CMDALT { get; set; }
            public string MISSILE_TECM_DETECT { get; set; }
            public string MISSILE_TECM_DETONATE { get; set; }
            public string MISSILE_TERM_ALT { get; set; }
            public string MISSILE_TGT_TURN_RATE { get; set; }
            public string MISSILE_TG_ALTITUDE { get; set; }
            public string MISSILE_TG_AMPLITUDE { get; set; }
            public string MISSILE_TG_PERIOD { get; set; }
            public string MISSILE_TG_STRT_RANGE { get; set; }
            public string MISSILE_TG_TYPE { get; set; }
            public string MISSILE_TRANS_POWER { get; set; }
            public string MISSILE_VEOCS_FRONT { get; set; }
            public string MISSILE_VEOCS_SIDE { get; set; }
            public string MISSILE_VERT_ACCEL { get; set; }
            public string MISSILE_WAYPOINT_LEG_LEN { get; set; }
            public string MISSILE_WAYPOINT_MAX { get; set; }
            public string MISSILE_WAYPOINT_REQ { get; set; }
            public string MISSILE_WEIGHT { get; set; }
            public string MISSILE_WHD_FUSE_FACTOR { get; set; }
            public string MISSILE_WHD_WEIGHT { get; set; }
            public string TARGET_DOMAIN { get; set; }
            public string TYPE_DESC { get; set; }
            public string jarak_tembak { get; set; }
            public string jarak_tembak_min { get; set; }
            public string kecepatan_awal { get; set; }
            public string name { get; set; }
            public string negara_pembuat { get; set; }
            public string perusahaan_pembuat { get; set; }
            public string radius_damage { get; set; }
            public string tahun_pembuat { get; set; }
        }

        public class BombWeapon
        {
            public string AIRCRAFT_ID { get; set; }
            public string SHIP_ID { get; set; }
            public string BOMB_AMMO_QTY { get; set; }
            public string BOMB_COUNT { get; set; }
            public string BOMB_DAMAGE { get; set; }
            public string BOMB_ID { get; set; }
            public string BOMB_LETHALITY { get; set; }
            public string BOMB_NAME { get; set; }
            public string BOMB_POINT_1_PROB { get; set; }
            public string BOMB_POINT_1_RANGE { get; set; }
            public string BOMB_POINT_2_PROB { get; set; }
            public string BOMB_POINT_2_RANGE { get; set; }
            public string BOMB_POINT_3_PROB { get; set; }
            public string BOMB_POINT_3_RANGE { get; set; }
            public string BOMB_POINT_4_PROB { get; set; }
            public string BOMB_POINT_4_RANGE { get; set; }
            public string BOMB_PROB_OF_HIT { get; set; }
            public string BOMB_RANGE_MAX { get; set; }
            public string BOMB_RANGE_MIN { get; set; }
            public string BOMB_TT_LAND { get; set; }
            public string BOMB_TT_SUB { get; set; }
            public string BOMB_TT_SURF { get; set; }
            public string BOMB_TYPE { get; set; }
            public string BOMB_TYPE_NAME { get; set; }
            public string BOMB_WARHEAD_WEIGHT { get; set; }
            public string COUNTRY_ID { get; set; }
            public string DATATYPE { get; set; }
            public string DESCRIPTION { get; set; }
            public string ID_WEAPON { get; set; }
            public string TYPE_DESC { get; set; }
            public string jarak_tembak { get; set; }
            public string jarak_tembak_min { get; set; }
            public string jumlah_peluru { get; set; }
            public string kecepatan_awal { get; set; }
            public string name { get; set; }
            public string negara_pembuat { get; set; }
            public string perusahaan_pembuat { get; set; }
            public string radius_damage { get; set; }
            public string tahun_pembuat { get; set; }
        }

        public class GunWeapon
        {
            public string AIRCRAFT_ID { get; set; }
            public string SHIP_ID { get; set; }
            public string COUNTRY_ID { get; set; }
            public string DATATYPE { get; set; }
            public string DESCRIPTION { get; set; }
            public string GUN_AIR_ENG_P1_POH { get; set; }
            public string GUN_AIR_ENG_P1_RG { get; set; }
            public string GUN_AIR_ENG_P2_POH { get; set; }
            public string GUN_AIR_ENG_P2_RG { get; set; }
            public string GUN_AIR_ENG_P3_POH { get; set; }
            public string GUN_AIR_ENG_P3_RG { get; set; }
            public string GUN_AIR_ENG_P4_POH { get; set; }
            public string GUN_AIR_ENG_P4_RG { get; set; }
            public string GUN_AUTO_FIRE { get; set; }
            public string GUN_CALIBER { get; set; }
            public string GUN_CATEGORY { get; set; }
            public string GUN_CATEGORY_NAME { get; set; }
            public string GUN_COUNT { get; set; }
            public string GUN_DAMAGE { get; set; }
            public string GUN_DAMAGE_RAD_EFF { get; set; }
            public string GUN_DAMAGE_RAD_MAX { get; set; }
            public string GUN_DEP_CHAFF { get; set; }
            public string GUN_DISPERSION_RAD { get; set; }
            public string GUN_ET_AIR { get; set; }
            public string GUN_ET_LAND { get; set; }
            public string GUN_ET_SURF { get; set; }
            public string GUN_FC_REQD { get; set; }
            public string GUN_FC_TYPE { get; set; }
            public string GUN_ID { get; set; }
            public string GUN_LENGTH { get; set; }
            public string GUN_LETHALITY { get; set; }
            public string GUN_MAGAZINE_CAPACITY { get; set; }
            public string GUN_MAGAZINE_WEIGHT { get; set; }
            public string GUN_MAX_ALT { get; set; }
            public string GUN_MAX_DEPRESS { get; set; }
            public string GUN_MAX_ELEV { get; set; }
            public string GUN_MAX_POH_AIR { get; set; }
            public string GUN_MAX_POH_SURF { get; set; }
            public string GUN_MUZLE_VEL { get; set; }
            public string GUN_NAME { get; set; }
            public string GUN_NGS_25_PDR { get; set; }
            public string GUN_NGS_CAP { get; set; }
            public string GUN_NGS_DEFLECT_ERR_MAX { get; set; }
            public string GUN_NGS_DEFLECT_ERR_MIN { get; set; }
            public string GUN_NGS_LETHALITY { get; set; }
            public string GUN_NGS_RANGE_MAX { get; set; }
            public string GUN_NGS_RANGE_MIN { get; set; }
            public string GUN_PROB_OF_HIT { get; set; }
            public string GUN_RANGE_AIR_MAX { get; set; }
            public string GUN_RANGE_EFFECTIVE { get; set; }
            public string GUN_RANGE_MIN { get; set; }
            public string GUN_RANGE_SURF_MAX { get; set; }
            public string GUN_ROF { get; set; }
            public string GUN_ROUND_DIAMETER { get; set; }
            public string GUN_SURF_ENG_P1_POH { get; set; }
            public string GUN_SURF_ENG_P1_RG { get; set; }
            public string GUN_SURF_ENG_P2_POH { get; set; }
            public string GUN_SURF_ENG_P2_RG { get; set; }
            public string GUN_SURF_ENG_P3_POH { get; set; }
            public string GUN_SURF_ENG_P3_RG { get; set; }
            public string GUN_SURF_ENG_P4_POH { get; set; }
            public string GUN_SURF_ENG_P4_RG { get; set; }
            public string GUN_TR_RATE_AZIMUTH { get; set; }
            public string GUN_TR_RATE_ELEV { get; set; }
            public string GUN_WEIGHT { get; set; }
            public string ID_WEAPON { get; set; }
            public string TYPE_DESC { get; set; }
            public string jarak_tembak { get; set; }
            public string jarak_tembak_min { get; set; }
            public string jumlah_peluru { get; set; }
            public string kecepatan_awal { get; set; }
            public string name { get; set; }
            public string negara_pembuat { get; set; }
            public string perusahaan_pembuat { get; set; }
            public string radius_damage { get; set; }
            public string tahun_pembuat { get; set; }
        }
        #endregion

        #region Detail Satuan Darat
        public class DetailSatuanDarat
        {
            [JsonProperty("object")]
            public ObjectDarat OBJ { get; set; }
            public ImageDarat images { get; set; }
            public string path_object_3d { get; set; }
            public string tipe_tni { get; set; }
            public RadarSatuan[] radar { get; set; }
            public GunWeapon[] gun { get; set; }
            public MissileWeapon[] missile { get; set; }
            public BombWeapon[] bomb { get; set; }
            public TorpedoWeapon[] torpedo { get; set; }

            public static DetailSatuanDarat FromJson(string json) => JsonConvert.DeserializeObject<DetailSatuanDarat>(json, HelperConverter.Converter.Settings);
        }

        public class ObjectDarat
        {
            [JsonProperty("VEHICLE_ID")]
            public int? id { get; set; }
            [JsonProperty("VEHICLE_SYM_ID")]
            public int? symbolID { get; set; }
            [JsonProperty("VEHICLE_NAME")]
            public string name { get; set; }
            [JsonProperty("VEHICLE_MAX_SPEED")]
            public string maxSpeed { get; set; }
            [JsonProperty("vehicle_matra")]
            public string matra { get; set; }
            [JsonProperty("VEHICLE_FUEL")]
            public string fuelLoad { get; set; }
            [JsonProperty("VEHICLE_FUEL_CAPACITY")]
            public string fuelCapacity { get; set; }
            [JsonProperty("VEHICLE_CONSUME_FUEL")]
            public string fuelConsume { get; set; }
            [JsonProperty("VEHICLE_HEIGHT")]
            public float? height { get; set; }
            [JsonProperty("VEHICLE_WIDTH")]
            public string width { get; set; }
            [JsonProperty("VEHICLE_WEIGHT")]
            public string weight { get; set; }
            [JsonProperty("VEHICLE_HEALTH")]
            public string health { get; set; }
            [JsonProperty("VEHICLE_LENGTH")]
            public string length { get; set; }
            [JsonProperty("image")]
            public ImageDarat image { get; set; }
            [JsonProperty("negara_pembuat")]
            public string negaraPembuat { get; set; }
            [JsonProperty("perusahaan_pembuat")]
            public string perusahaanPembuat { get; set; }
            [JsonProperty("tahun_pembuat")]
            public string tahunPembuat { get; set; }
            [JsonProperty("titan")]
            public string titan { get; set; }
            [JsonProperty("model_3d_tfg")]
            public string model3D { get; set; }
            [JsonProperty("VEHICLE_IS_AMFIBI")]
            public int? isAmfibi { get; set; }
            [JsonProperty("VEHICLE_TYPE_ID")]
            public int? typeID { get; set; }
            [JsonProperty("VEHICLE_TYPE_DESC")]
            public string typeDesc { get; set; }

            public string jenis { get; set; }
            public string style_symbol { get; set; }

            public static string ToString(ObjectDarat json) => JsonConvert.SerializeObject(json);
        }

        public class ImageDarat
        {
            public int? VEHICLE_ID { get; set; }
            public string VEHICLE_IMAGE_FNAME { get; set; }
            public string VEHICLE_IMAGE_DESC { get; set; }
        }
        #endregion

        #region Detail Satuan Laut
        public class DetailSatuanLaut
        {
            [JsonProperty("object")]
            public ObjectLaut OBJ { get; set; }
            //public ImageLaut? images { get; set; }
            public string path_object_3d { get; set; }
            public string tipe_tni { get; set; }
            public RadarSatuan[] radar { get; set; }
            public SonarSatuan[] sonar { get; set; }
            public GunWeapon[] gun { get; set; }
            public MissileWeapon[] missile { get; set; }
            public BombWeapon[] bomb { get; set; }
            public TorpedoWeapon[] torpedo { get; set; }

            public static DetailSatuanLaut FromJson(string json) => JsonConvert.DeserializeObject<DetailSatuanLaut>(json, HelperConverter.Converter.Settings);
        }

        public class ObjectLaut
        {
            [JsonProperty("SHIP_ID")]
            public int? id { get; set; }
            [JsonProperty("SHIP_SYM_ID")]
            public int? symbolID { get; set; }
            [JsonProperty("SHIP_NAME")]
            public string name { get; set; }
            [JsonProperty("SHIP_MAX_SPEED")]
            public string maxSpeed { get; set; }
            [JsonProperty("ship_matra")]
            public string matra { get; set; }
            [JsonProperty("SHIP_FUEL_LOAD")]
            public float? fuelLoad { get; set; }
            [JsonProperty("SHIP_FUEL_MAX")]
            public float? fuelMax { get; set; }
            [JsonProperty("SHIP_FUEL_CAPACITY")]
            public float? fuelCapacity { get; set; }
            [JsonProperty("SHIP_CONSUME_FUEL")]
            public float? fuelConsume { get; set; }
            [JsonProperty("SHIP_DIM_LENGTH")]
            public float? length { get; set; }
            [JsonProperty("SHIP_DIM_HEIGHT")]
            public float? height { get; set; }
            [JsonProperty("SHIP_DIM_WIDTH")]
            public float? width { get; set; }
            [JsonProperty("image")]
            public ImageLaut image { get; set; }
            [JsonProperty("SHIP_CATEGORY_ID")]
            public string categoryID { get; set; }
            [JsonProperty("SHIP_CATEGORY_NAME")]
            public string categoryName { get; set; }
            [JsonProperty("SHIP_CATEGORY_DESC")]
            public string categoryDesc { get; set; }
            [JsonProperty("SHIP_CLASS_ID")]
            public int? classID { get; set; }
            [JsonProperty("SHIP_CLASS_NAME")]
            public string className { get; set; }
            [JsonProperty("SHIP_HEALTH")]
            public float? health { get; set; }
            [JsonProperty("SHIP_MAX_LOAD_QTY")]
            public string maxLoadQty { get; set; }
            [JsonProperty("negara_pembuat")]
            public string negaraPembuat { get; set; }
            [JsonProperty("perusahaan_pembuat")]
            public string perusahaanPembuat { get; set; }
            [JsonProperty("tahun_pembuat")]
            public string tahunPembuat { get; set; }
            [JsonProperty("JUMLAH_PERWIRA")]
            public string jumlah_perwira { get; set; }
            [JsonProperty("JUMLAH_TAMTAMA")]
            public string jumlah_tamtama { get; set; }
            [JsonProperty("JUMLAH_BINTARA")]
            public string jumlah_bintara { get; set; }
            [JsonProperty("titan")]
            public string titan { get; set; }
            [JsonProperty("model_3d_tfg")]
            public string model3D { get; set; }

            public string jenis { get; set; }
            public string style_symbol { get; set; }

            public static string ToString(ObjectLaut json) => JsonConvert.SerializeObject(json);
        }

        public class ImageLaut
        {
            public int? SHIP_ID { get; set; }
            public string SHIP_IMAGE_FNAME { get; set; }
            public string SHIP_IMAGE_DESC { get; set; }
        }
        #endregion

        #region Detail Satuan Udara
        public class DetailSatuanUdara
        {
            [JsonProperty("object")]
            public ObjectUdara OBJ { get; set; }
            public ImageUdara images { get; set; }
            public string path_object_3d { get; set; }
            public string tipe_tni { get; set; }
            public RadarSatuan[] radar { get; set; }
            public GunWeapon[] gun { get; set; }
            public MissileWeapon[] missile { get; set; }
            public BombWeapon[] bomb { get; set; }
            public TorpedoWeapon[] torpedo { get; set; }

            public static DetailSatuanUdara FromJson(string json) => JsonConvert.DeserializeObject<DetailSatuanUdara>(json, HelperConverter.Converter.Settings);
        }

        public class ObjectUdara
        {
            [JsonProperty("AIRCRAFT_ID")]
            public int? id { get; set; }
            [JsonProperty("AIRCRAFT_SYM_ID")]
            public int? symbolID { get; set; }
            [JsonProperty("AIRCRAFT_NAME")]
            public string name { get; set; }
            [JsonProperty("AIRCRAFT_MAX_SPEED")]
            public float? maxSpeed { get; set; }
            [JsonProperty("aircraft_matra")]
            public string matra { get; set; }
            //[JsonProperty("AIRCRAFT_FUEL_CAPACITY")]
            //public float? fuelCapacity { get; set; }
            [JsonProperty("AIRCRAFT_CONSUME_FUEL")]
            public float? fuelConsume { get; set; }
            [JsonProperty("AIRCRAFT_HEIGHT")]
            public float? height { get; set; }
            [JsonProperty("AIRCRAFT_WEIGHT")]
            public float? weight { get; set; }
            [JsonProperty("AIRCRAFT_HEALTH")]
            public float? health { get; set; }
            [JsonProperty("AIRCRAFT_TYPE")]
            public int? type { get; set; }
            [JsonProperty("AIRCRAFT_TYPE_DESC")]
            public string typeDesc { get; set; }
            [JsonProperty("image")]
            public ImageUdara image { get; set; }
            [JsonProperty("negara_pembuat")]
            public string negaraPembuat { get; set; }
            [JsonProperty("perusahaan_pembuat")]
            public string perusahaanPembuat { get; set; }
            [JsonProperty("tahun_pembuat")]
            public string tahunPembuat { get; set; }
            [JsonProperty("titan")]
            public string titan { get; set; }
            [JsonProperty("model_3d_tfg")]
            public string model3D { get; set; }

            public string jenis { get; set; }
            public string style_symbol { get; set; }

            public static string ToString(ObjectUdara json) => JsonConvert.SerializeObject(json);
        }

        public class ImageUdara
        {
            public int? AIRCRAFT_ID { get; set; }
            public string AIRCRAFT_IMAGE_FNAME { get; set; }
            public string AIRCRAFT_IMAGE_DESC { get; set; }
        }
        #endregion

        public class Detector
        {
            public Detector(RadarSatuan[] dataRadar, SonarSatuan[] dataSonar)
            {
                this.dataRadar = dataRadar;
                this.dataSonar = dataSonar;
            }

            public Detector() { }

            public RadarSatuan[] dataRadar { get; set; }
            public SonarSatuan[] dataSonar { get; set; }
            public static Detector FromJson(string json) => JsonConvert.DeserializeObject<Detector>(json, HelperConverter.Converter.Settings);
        }
    }
}

// --- CONVERTER HELPER (Untuk Seluruh Helper) ---
public partial class HelperConverter
{
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

    }
}
// -----------------------------------------------