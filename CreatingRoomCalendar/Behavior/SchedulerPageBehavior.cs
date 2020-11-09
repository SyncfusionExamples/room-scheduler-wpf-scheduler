using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfSchedule.XForms;
using Xamarin.Forms;

namespace ScheduleGettingStarted
{
    /// <summary>
    /// Appointment Editor Behavior class.
    /// </summary>
    internal class SchedulerPageBehavior : Behavior<ContentPage>
    {
        #region Fields
      
        /// <summary>
        /// editor layout.
        /// </summary>
        private EditorLayout editorLayout;

        /// <summary>
        /// Gets or sets schedule.
        /// </summary>
        private Syncfusion.SfSchedule.XForms.SfSchedule schedule
        {
            get;
            set;
        }

        #endregion

        #region OnAttached

        /// <summary>
        /// Begins when the behavior attached to the view. 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnAttachedTo(ContentPage bindable)
        {
            this.schedule = (bindable.Content as Grid).Children[0] as Syncfusion.SfSchedule.XForms.SfSchedule;

            this.editorLayout = (bindable.Content as Grid).Children[1] as EditorLayout;

            this.schedule.TimelineViewSettings.AppointmentHeight = 1000;
           
            if (Device.RuntimePlatform == Device.Android)
            {
                this.schedule.ViewHeaderStyle.DateFontSize = 24;
            }

            this.schedule.CellDoubleTapped += this.OnSchedulerCellDoubleTapped;
            (this.editorLayout.BindingContext as EditorLayoutViewModel).AppointmentModified += this.OnEditorLayoutAppointmentModified;
            (this.editorLayout.Behaviors[0] as EditorLayoutBehavior).AddEditorElements();
        }

        #endregion

        #region OnDetachingFrom

        /// <summary>
        /// Begins when the behavior attached to the view
        /// </summary>
        /// <param name="bindable">bindable param</param>
        protected override void OnDetachingFrom(ContentPage bindable)
        {
            this.schedule.CellDoubleTapped -= this.OnSchedulerCellDoubleTapped;
            (this.editorLayout.BindingContext as EditorLayoutViewModel).AppointmentModified -= this.OnEditorLayoutAppointmentModified;

            this.schedule = null;
            this.editorLayout = null;
        }
        #endregion

        #region EditorLayout_AppointmentModified

        /// <summary>
        /// editor layout
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Schedule Appointment Modified Event args</param>
        private void OnEditorLayoutAppointmentModified(object sender, ScheduleAppointmentModifiedEventArgs e)
        {
            this.editorLayout.IsVisible = false;
            if (e.IsModified)
            {
                (this.schedule.DataSource as ObservableCollection<Meeting>).Add(e.Appointment);
            }
        }
        #endregion

        #region CellDoubleTapped

        /// <summary>
        /// Method for cell double tapped
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="args">cell tapped event args</param>
        private void OnSchedulerCellDoubleTapped(object sender, Syncfusion.SfSchedule.XForms.CellTappedEventArgs args)
        {
            if (args.Appointment == null)
            {
                this.editorLayout.IsVisible = true;
                (this.editorLayout.Behaviors[0] as EditorLayoutBehavior).OpenEditor(null, args.Datetime, args.Resource);
            }
        }
        #endregion      
    }
}
