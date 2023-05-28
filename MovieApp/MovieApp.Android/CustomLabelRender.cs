using Android.Content;
using Android.OS;
using Android.Text;
using MovieApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLabel.CustomLabel), typeof(CustomLabelRender))]
namespace MovieApp.Droid
{
    public class CustomLabelRender : LabelRenderer
    {
        public CustomLabelRender(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null && Build.VERSION.SdkInt > BuildVersionCodes.M)
            {
                Control.JustificationMode = JustificationMode.InterWord;
            }
        }
    }
}