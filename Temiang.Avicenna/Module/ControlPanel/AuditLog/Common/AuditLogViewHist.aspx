<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="AuditLogViewHist.aspx.cs" Inherits="Temiang.Avicenna.ControlPanel.AuditLogViewHist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script type="text/javascript">
        function OnDataBound(sender, args) {
            var items = sender.get_items();
            sender.expand(items[0]);
            sender.expand(items[1]);
        }

    </script>
    <telerik:RadTimeline runat="server" ID="timelineEdit" CollapsibleEvents="true"
        DataDateField="LogDateTime" AlternatingMode="true" 
        DataTitleField="Title"
        DataKeyNames="CurrentValue,UserName">
        <ClientEvents OnDataBound="OnDataBound" />
        <EventTemplate>
                <div class="k-card-header">
                    <h5 class="k-card-title">
                        <span class="k-event-title">#= data.Title #</span>
                          <span class="k-event-collapse k-button k-button-icon k-flat"><span class="k-icon k-i-arrow-chevron-right"></span></span>
                    </h5>
                    <h6 class="k-card-subtitle">By: <strong>#= data.UserName #</strong></h6>
                </div>
                <div class="k-card-body">
                    <div class="k-card-description">
                        #= data.CurrentValue #
                    </div>
                </div>
        </EventTemplate>
    </telerik:RadTimeline>
</asp:Content>