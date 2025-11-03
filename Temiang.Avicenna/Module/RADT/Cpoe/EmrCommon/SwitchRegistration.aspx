<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="SwitchRegistration.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Emr.EmrCommon.SwitchRegistration" Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function OnDataBound(sender, args) {
            sender.expand(sender.get_items()[0]);
        }

        function openEmrDetail(url) {
            if (url == '') return;

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            //property argument harus didefinisikan supaya bisa ditambah child propertinya contoh oWnd.argument.rebind = 'rebind';
            //kalau tidak maka script tidak akan dijalankan ke baris berikutnya ini tidak tahu knp padahal sebelum2nya tidak perlu
            oWnd.argument = new Object();

            var oArg = new Object();
            oArg.callbackMethod = "redirect";
            oArg.url = url;

            //Close the RadWindow            
            oWnd.close(oArg);
        }
    </script>
    <telerik:RadTimeline runat="server" ID="timelineReg" CollapsibleEvents="true"
        DataDateField="RegistrationDateTime" AlternatingMode="true"
        DataTitleField="ServiceUnitName"
        DataKeyNames="RegistrationDate,RegistrationNo,ParamedicName,LOSInDay, Description, Url,SwitchCaption">
        <ClientEvents OnDataBound="OnDataBound" />
        <EventTemplate>
                <div class="k-card-header">
                    <h5 class="k-card-title">
                        <span class="k-event-title">#= data.ServiceUnitName #</span>
                          <span class="k-event-collapse k-button k-button-icon k-flat"><span class="k-icon k-i-arrow-chevron-right"></span></span>
                    </h5>
                    <h6 class="k-card-subtitle">Physician: <strong>#= data.ParamedicName #</strong></h6>
                    <h7 class="k-card-subtitle">Date: <strong>#= data.RegistrationDate #</strong></h7>
                    <h7 class="k-card-subtitle">RegNo: <strong>#= data.RegistrationNo #</strong></h7>
                    <h7 class="k-card-subtitle">LOS: <strong>#= data.LOSInDay??"-" #</strong>&nbsp;Day</h7>
                </div>
                <div class="k-card-body">
                    <div class="k-card-description">
                        #= data.Description #
                    </div>
                </div>
 
                <div class="k-card-actions">
                    <a class="k-button k-flat k-primary" onclick="javascript:openEmrDetail('#= data.Url#');return false;"  target="_blank">#= data.SwitchCaption #</a>
                </div>
        </EventTemplate>
<%--        <WebServiceClientDataSource>
            <Schema>
                <Model>
                    <telerik:ClientDataSourceModelField DataType="Date" FieldName="RegistrationDate" />
                </Model>
            </Schema>
            <SortExpressions>
                <telerik:ClientDataSourceSortExpression FieldName="RegistrationDate" SortOrder="Desc" />
                <telerik:ClientDataSourceSortExpression FieldName="RegistrationNo" SortOrder="Desc" />
            </SortExpressions>
        </WebServiceClientDataSource>--%>
    </telerik:RadTimeline>
</asp:Content>
