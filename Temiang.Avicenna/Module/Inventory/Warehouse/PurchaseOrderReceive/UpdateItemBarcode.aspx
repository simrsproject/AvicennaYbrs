<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="UpdateItemBarcode.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.UpdateItemBarcode"
    Title="Update Barcode" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>Old Barcode</legend>
        <telerik:RadTextBox ID="txtBarcode" runat="server" Width="300px" Font-Size="24px" Enabled="False" />
    </fieldset>
    <br />
    <fieldset>
        <legend>New Barcode</legend>
        <telerik:RadTextBox ID="txtBarcodeEntry" runat="server" Width="300px" 
            Font-Size="24px"  />
    </fieldset>
</asp:Content>
