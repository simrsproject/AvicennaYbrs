<%@  Title="Import" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="AppointmentImportExcelDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.OutPatient.AppointmentImportExcelDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label">
                Guarantor
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" EnableLoadOnDemand="true"
                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboGuarantorID_ItemDataBound"
                    OnItemsRequested="cboGuarantorID_ItemsRequested">
                    <FooterTemplate>
                        Note : Show max 10 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20" />
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                Excel File
            </td>
            <td class="entry">
                <asp:FileUpload ID="fileuploadExcel" runat="server" />
            </td>
            <td width="20" />
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
