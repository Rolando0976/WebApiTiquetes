namespace WebApiTiquetes.Models
{
    public class Ticketes
    {
        public int ti_identificador { set; get; }
        public string ti_asunto {  set; get; }
        public string ti_categoria { set; get; }
        public int? ti_us_id_asigna {  set; get; }
        public string ti_urgencia { set; get; }
        public string ti_importancia { set; get; }
        public string ti_estado { set; get; }
        public DateTime ti_fecha_adicion {  set; get; }
        public string ti_adicionado_por {  set; get; }
        public DateTime? ti_fecha_modificacion { set; get; }
        public string? ti_modificado_por { set; get; }
    }
}
