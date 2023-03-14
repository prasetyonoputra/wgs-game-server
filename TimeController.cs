using System;
using System.Threading.Tasks;
using UnityEngine;
using Wargaming.Core.GlobalParam.HelperScenarioAktif;
using Wargaming.Core.Network;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;
    public SkenarioAktifWaktu skenario;
    public DateTime  tanggalMulai, tanggalSelesai, waktuSekarang;
    public float elapsed = 0f, percepatan = 1f;
    public bool isPlay = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async Task Init()
    {
        var jsonSkenario = await WargamingAPI.GetSkenarioAktif2();
        string x = jsonSkenario.Substring(1);
        string result = x.Remove(x.Length - 1);

        skenario = SkenarioAktifWaktu.FromJson(result);

        tanggalMulai = Convert.ToDateTime(skenario.tgl_mulai_asum);
        tanggalSelesai = Convert.ToDateTime(skenario.tgl_selesai_asum);
        waktuSekarang = tanggalMulai;
    }

    private void Update()
    {
        if (isPlay)
        {
            elapsed += Time.deltaTime * percepatan;
            if (elapsed >= 1f)
            {
                elapsed = elapsed % 1f;
                waktuSekarang = waktuSekarang.AddSeconds(1);
                Debug.Log(waktuSekarang.ToString());

                ColyseusController.instance.SetTime(waktuSekarang.ToString());
            }
        }
    }

    public DateTimeOffset getDateTimeOffset()
    {
        return waktuSekarang;
    }
}
