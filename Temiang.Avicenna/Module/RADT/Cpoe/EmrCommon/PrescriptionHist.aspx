<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PrescriptionHist.aspx.cs" Inherits="Temiang.Avicenna.Module.Emr.EmrCommon.PrescriptionHist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script type="text/javascript">
        function OnDataBound(sender, args) {
            sender.expand(sender.get_items()[0]);
        }

    </script>
    <telerik:RadTimeline runat="server" ID="timelinePrescription" CollapsibleEvents="true"
        DataDateField="PrescriptionDate" AlternatingMode="true"
        DataTitleField="Title"
        DataKeyNames="ParamedicName, PrescriptionDateLabel, PrescriptionItem, Url">
        <ClientEvents OnDataBound="OnDataBound" />
        <EventTemplate>
                <div class="k-card-header">
                    <h5 class="k-card-title">
                        <span class="k-event-title">#= data.Title #</span>
                          <span class="k-event-collapse k-button k-button-icon k-flat"><span class="k-icon k-i-arrow-chevron-right"></span></span>
                    </h5>
                    <h6 class="k-card-subtitle">Physician: <strong>#= data.ParamedicName #</strong></h6>
                    <h7 class="k-card-subtitle">Date: <strong>#= data.PrescriptionDateLabel #</strong></h7>
                </div>
                <div class="k-card-body">
                    <div class="k-card-description">
                        #= data.PrescriptionItem #
                    </div>
                </div>
 
<%--                <div class="k-card-actions">
                    <a class="k-button k-flat k-primary" onclick="javascript:openEmrDetail('#= data.Url#');return false;"  target="_blank">Open</a>
                </div>--%>
        </EventTemplate>
        
        <WebServiceClientDataSource>
            <Schema>
                <Model>
                    <telerik:ClientDataSourceModelField DataType="Date" FieldName="PrescriptionDate" />
                </Model>
            </Schema>
            <SortExpressions>
                <telerik:ClientDataSourceSortExpression FieldName="PrescriptionDate" SortOrder="Desc" />
            </SortExpressions>
        </WebServiceClientDataSource>
    </telerik:RadTimeline>
</asp:Content>