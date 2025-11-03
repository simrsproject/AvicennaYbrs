<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EpisodeDiagnoseItemCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.Common.EpisodeDiagnoseItemCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<table>
    <tr>
        <td class="label" style="width: 10px" >
            <asp:Label runat="server" ID="lblNo"></asp:Label></td>
        <td class="label" style="width: 100px">Type</td>
        <td>
            <telerik:RadComboBox ID="cboSRDiagnoseType" runat="server" Width="200px" /><asp:CheckBox ID="chkIsOldCase" runat="server" Text="Old Case" Font-Italic="True" EnableViewState="True" />
        </td>
        <td></td>
    </tr>
    <tr>
        <td rowspan="2"> <%= ReadOnly || RowNo==1? string.Empty:string.Format("<a style=\"cursor: pointer;\" OnClick=\"javascript:__doPostBack('{1}', 'DEL_{2}'); return true;\" ><img src=\"{0}/Images/Toolbar/row_delete16.png\"/></a>",Helper.UrlRoot(),cboDiagnoseID.UniqueID,this.ID.Split('_')[1]) %></td>
        <td class="label" style="width: 100px">ICD X</td>
        <td >
            <telerik:RadCodeBlock runat="server">
                <script type="text/javascript">
                    function onDiagnoseNameClick(name) {
                        var txt = $find("<%= txtDiagnoseText.ClientID %>");
                        txt.set_value(name);
                    }
                </script>
            </telerik:RadCodeBlock>
            <telerik:RadComboBox ID="cboDiagnoseID" runat="server" Width="300px" EmptyMessage="Select a Diagnosis"
                EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                <WebServiceSettings Method="DiagnosePerSmf" Path="~/WebService/ComboBoxDataService.asmx" />
                <ClientItemTemplate>
                    <div onclick="onDiagnoseNameClick('#= Attributes.DiagnoseName #')">
                        <ul class="details">
                            <li class="bold"><span>#= Value #</span></li>
                            <li class="small"><span>#= Attributes.DiagnoseName #</span></li>
                            <li class="smaller"><span>Syn: #= Attributes.Synonym #  </span></li>
                            <li class="smaller"><span>DTD: [#= Attributes.DtdNo #] #= Attributes.DtdName #  </span></li>
                        </ul>
                    </div>
                </ClientItemTemplate>
            </telerik:RadComboBox>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label" style="width: 100px">Diagnose</td>
        <td>
            <telerik:RadTextBox ID="txtDiagnoseText" runat="server" Width="300px" MaxLength="250"
                TextMode="MultiLine" /></td>
        <td></td>
    </tr>
    <tr>

</table>

