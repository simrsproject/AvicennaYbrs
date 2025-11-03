<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DietPatientsItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.DietPatientsItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumDietItem" runat="server" ValidationGroup="DietItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="DietItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDietID" runat="server" Text="Diet"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboDietID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboDietID_ItemDataBound" OnItemsRequested="cboDietID_ItemsRequested"
                            OnSelectedIndexChanged="cboDietID_SelectedIndexChanged" AutoPostBack="True">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "DietName")%>
                                    &nbsp;(<%# DataBinder.Eval(Container.DataItem, "DietID")%>) </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvDietID" runat="server" ErrorMessage="Diet required."
                            ControlToValidate="cboDietID" SetFocusOnError="True" ValidationGroup="DietItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMenu" runat="server" Text="Menu"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtMenu" runat="server" Width="300px" ReadOnly="True" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvMenu" runat="server" ErrorMessage="Menu required."
                            ControlToValidate="txtMenu" SetFocusOnError="True" ValidationGroup="DietItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblExtraQty" runat="server" Text="Liquid Food Qty"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtExtraQty" runat="server" Width="100px" MaxLength="10"
                            MinValue="0" NumberFormat-DecimalDigits="0" AutoPostBack="False"
                            OnTextChanged="txtExtraQty_TextChanged" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr style="display:none">
                    <td class="label">
                        <asp:Label ID="lblLiquidTime" runat="server" Text="Liquid Food Schedule"></asp:Label>
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIs09" Text="09:00" runat="server" AutoPostBack="True" OnCheckedChanged="chkIs09_CheckedChanged" />
                        &nbsp;&nbsp;<asp:CheckBox ID="chkIs15" Text="15:00" runat="server" AutoPostBack="True"
                            OnCheckedChanged="chkIs09_CheckedChanged" />
                        &nbsp;&nbsp;<asp:CheckBox ID="chkIs21" Text="21:00" runat="server" AutoPostBack="True"
                            OnCheckedChanged="chkIs09_CheckedChanged" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label2" runat="server" Text="Liquid Food Schedule"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadAutoCompleteBox ID="acbLiquidTime" runat="server" Width="300px" DropDownHeight="150">
                        </telerik:RadAutoCompleteBox>
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="DietItem"
                            CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="DietItem" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            OnClick="btnCancel_ButtonClick" CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td></td>
                    <td class="entry" colspan="3">
                        <table width="100%">
                            <tr>
                                <td style="width: 35%" class="labelcaption">
                                    <b>
                                        <asp:Label ID="Label1" runat="server" Text="Value"></asp:Label></b>
                                </td>
                                <td style="width: 20%" class="labelcaption">
                                    <b>
                                        <asp:Label ID="Label3" runat="server" Text="Interval"></asp:Label></b>
                                </td>
                                <td style="width: 45%" class="labelcaption">
                                    <b>
                                        <asp:Label ID="Label4" runat="server" Text="Range"></asp:Label></b>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCalorie" runat="server" Text="Calorie"></asp:Label>
                    </td>
                    <td class="entry" colspan="3">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 25%">
                                    <telerik:RadNumericTextBox ID="txtCalorie" runat="server" Width="100px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                                <td style="width: 10%">
                                    <asp:ImageButton ID="imgCaloriePlus" runat="server" ImageUrl="../../../../Images/Toolbar/arrowup_blue16.png"
                                        CausesValidation="False" OnClick="imgCaloriePlus_Click" />&nbsp;
                                    <asp:ImageButton ID="imgCalorieMin" runat="server" ImageUrl="../../../../Images/Toolbar/arrowdown_blue16.png"
                                        CausesValidation="False" OnClick="imgCalorieMin_Click" />
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadNumericTextBox ID="txtCalorieInterval" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                                <td style="width: 45%">
                                    <telerik:RadNumericTextBox ID="txtCalorieMin" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                    &nbsp;to&nbsp;
                                    <telerik:RadNumericTextBox ID="txtCalorieMax" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblProtein" runat="server" Text="Protein"></asp:Label>
                    </td>
                    <td class="entry" colspan="3">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 25%">
                                    <telerik:RadNumericTextBox ID="txtProtein" runat="server" Width="100px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                                <td style="width: 10%">
                                    <asp:ImageButton ID="imgProteinPlus" runat="server" ImageUrl="../../../../Images/Toolbar/arrowup_blue16.png"
                                        CausesValidation="False" OnClick="imgProteinPlus_Click" />&nbsp;
                                    <asp:ImageButton ID="imgProteinMin" runat="server" ImageUrl="../../../../Images/Toolbar/arrowdown_blue16.png"
                                        CausesValidation="False" OnClick="imgProteinMin_Click" />
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadNumericTextBox ID="txtProteinInterval" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                                <td style="width: 45%">
                                    <telerik:RadNumericTextBox ID="txtProteinMin" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                    &nbsp;to&nbsp;
                                    <telerik:RadNumericTextBox ID="txtProteinMax" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFat" runat="server" Text="Fat"></asp:Label>
                    </td>
                    <td class="entry" colspan="3">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 25%">
                                    <telerik:RadNumericTextBox ID="txtFat" runat="server" Width="100px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                                <td style="width: 10%">
                                    <asp:ImageButton ID="imgFatPlus" runat="server" ImageUrl="../../../../Images/Toolbar/arrowup_blue16.png"
                                        CausesValidation="False" OnClick="imgFatPlus_Click" />&nbsp;
                                    <asp:ImageButton ID="imgFatMin" runat="server" ImageUrl="../../../../Images/Toolbar/arrowdown_blue16.png"
                                        CausesValidation="False" OnClick="imgFatMin_Click" />
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadNumericTextBox ID="txtFatInterval" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                                <td style="width: 45%">
                                    <telerik:RadNumericTextBox ID="txtFatMin" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                    &nbsp;to&nbsp;
                                    <telerik:RadNumericTextBox ID="txtFatMax" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCarbohydrate" runat="server" Text="Carbohydrate"></asp:Label>
                    </td>
                    <td class="entry" colspan="3">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 25%">
                                    <telerik:RadNumericTextBox ID="txtCarbohydrate" runat="server" Width="100px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                                <td style="width: 10%">
                                    <asp:ImageButton ID="imgCarbohydratePlus" runat="server" ImageUrl="../../../../Images/Toolbar/arrowup_blue16.png"
                                        CausesValidation="False" OnClick="imgCarbohydratePlus_Click" />&nbsp;
                                    <asp:ImageButton ID="imgCarbohydrateMin" runat="server" ImageUrl="../../../../Images/Toolbar/arrowdown_blue16.png"
                                        CausesValidation="False" OnClick="imgCarbohydrateMin_Click" />
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadNumericTextBox ID="txtCarbohydrateInterval" runat="server" Width="80px"
                                        MaxLength="10" MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                                <td style="width: 45%">
                                    <telerik:RadNumericTextBox ID="txtCarbohydrateMin" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                    &nbsp;to&nbsp;
                                    <telerik:RadNumericTextBox ID="txtCarbohydrateMax" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSalt" runat="server" Text="Salt"></asp:Label>
                    </td>
                    <td class="entry" colspan="3">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 25%">
                                    <telerik:RadNumericTextBox ID="txtSalt" runat="server" Width="100px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                                <td style="width: 10%">
                                    <asp:ImageButton ID="imgSaltPlus" runat="server" ImageUrl="../../../../Images/Toolbar/arrowup_blue16.png"
                                        CausesValidation="False" OnClick="imgSaltPlus_Click" />&nbsp;
                                    <asp:ImageButton ID="imgSaltMin" runat="server" ImageUrl="../../../../Images/Toolbar/arrowdown_blue16.png"
                                        CausesValidation="False" OnClick="imgSaltMin_Click" />
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadNumericTextBox ID="txtSaltInterval" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                                <td style="width: 45%">
                                    <telerik:RadNumericTextBox ID="txtSaltMin" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                    &nbsp;to&nbsp;
                                    <telerik:RadNumericTextBox ID="txtSaltMax" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFiber" runat="server" Text="Fiber"></asp:Label>
                    </td>
                    <td class="entry" colspan="3">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 25%">
                                    <telerik:RadNumericTextBox ID="txtFiber" runat="server" Width="100px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                                <td style="width: 10%">
                                    <asp:ImageButton ID="imgFiberPlus" runat="server" ImageUrl="../../../../Images/Toolbar/arrowup_blue16.png"
                                        CausesValidation="False" OnClick="imgFiberPlus_Click" />&nbsp;
                                    <asp:ImageButton ID="imgFiberMin" runat="server" ImageUrl="../../../../Images/Toolbar/arrowdown_blue16.png"
                                        CausesValidation="False" OnClick="imgFiberMin_Click" />
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadNumericTextBox ID="txtFiberInterval" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                                <td style="width: 45%">
                                    <telerik:RadNumericTextBox ID="txtFiberMin" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                    &nbsp;to&nbsp;
                                    <telerik:RadNumericTextBox ID="txtFiberMax" runat="server" Width="80px" MaxLength="10"
                                        MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
