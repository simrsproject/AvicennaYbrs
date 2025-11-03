<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SoapInfoCtl.ascx.cs"
    Inherits="Temiang.Avicenna.CustomControl.SoapInfoCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style>
    .RadGrid_Default {
        border-color: #ccc;
        color: #333;
        background-color: #fff;
        font-family: "Segoe UI",Arial,Helvetica,sans-serif;
        font-size: 12px;
        line-height: 16px;
    }

    .RadGrid {
        border-width: 1px;
        border-style: solid;
    }

        .RadGrid .rgClipCells .rgHeader {
            overflow: hidden;
        }

    .RadGrid_Default .rgHeader {
        color: #333;
        border: 0;
        border-bottom: 1px solid #828282;
        background: #eaeaea 0 -2300px repeat-x url('WebResource.axd?d=dIbqQ_qfp2B5-I4gZSJx5HTQzsWbkrGmy6h2Z1ADKMG7z4b5Cbq6RKV68yURyBCW3ZIL10mnlQyRj_TgM0fmuLXRmy8xOQGMzmnZiypyOIhj8gzcOaG4IDgPxDMsOvEAbBdx5_NT4yKMiWLDjYy7UA2&t=637729163414467936');
    }

    .RadGrid .rgHeader {
        padding-top: 5px;
        padding-bottom: 4px;
        text-align: left;
        font-weight: normal;
        padding-left: 7px;
        padding-right: 7px;
        cursor: default;
    }

    .RadGrid_Default .rgMasterTable {
        font-family: "Segoe UI",Arial,Helvetica,sans-serif;
        font-size: 12px;
        line-height: 16px;
    }

    .RadGrid .rgMasterTable {
        border-collapse: separate;
        border-spacing: 0;
    }

    .RadGrid_Default .rgAltRow {
        background: #f2f2f2;
    }
</style>
<fieldset>
    <legend><b>SOAP History</b></legend>
    <div class="RadGrid RadGrid_Default">
        <asp:Literal runat="server" ID="litSoap"></asp:Literal>
    </div>
</fieldset>
