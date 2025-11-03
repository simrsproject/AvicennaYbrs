<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeRL4Detail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeeRL4Detail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeRL4" runat="server" ValidationGroup="EmployeeRL4" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeRL4"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblEmployeeRL4ID" runat="server" Text="Employee RL4 ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEmployeeRL4ID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEmployeeRL4ID" runat="server" ErrorMessage="Employee RL4 ID required."
                            ControlToValidate="txtEmployeeRL4ID" SetFocusOnError="True" ValidationGroup="EmployeeRL4" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblCompanyEducationProfileID" runat="server" Text="Education Profile Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboCompanyEducationProfileID" runat="server" Width="300px"
                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                            AutoPostBack="false" OnItemDataBound="cboCompanyEducationProfileID_ItemDataBound"
                            OnItemsRequested="cboCompanyEducationProfileID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "CompanyEducationProfileCode")%>
                                &nbsp;-&nbsp;
                                <%# DataBinder.Eval(Container.DataItem, "CompanyEducationProfileName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <%--<asp:RequiredFieldValidator ID="rfvCompanyEducationProfileID" runat="server" ErrorMessage="Company Education Profile Name required."
            	            ControlToValidate="cboCompanyEducationProfileID" SetFocusOnError="True" ValidationGroup="EmployeeRL4" Width="100%">
				            <asp:Image ID="Image10" runat="server" SkinID="rfvImage"/>
			            </asp:RequiredFieldValidator>--%>
                    </td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblCompanyFieldOfWorkProfileID" runat="server" Text="Field Of Work Profile Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboCompanyFieldOfWorkProfileID" runat="server" Width="300px"
                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                            AutoPostBack="false" OnItemDataBound="cboCompanyFieldOfWorkProfileID_ItemDataBound"
                            OnItemsRequested="cboCompanyFieldOfWorkProfileID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "CompanyFieldOfWorkProfileCode")%>
                                &nbsp;-&nbsp;
                                <%# DataBinder.Eval(Container.DataItem, "CompanyFieldOfWorkProfileName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <%--<asp:RequiredFieldValidator ID="rfvCompanyFieldOfWorkProfileID" runat="server" ErrorMessage="Field Of Work Profile Name required."
            	            ControlToValidate="cboCompanyFieldOfWorkProfileID" SetFocusOnError="True" ValidationGroup="EmployeeRL4" Width="100%">
				            <asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
			            </asp:RequiredFieldValidator>--%>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRL4Status" runat="server" Text="Labor Status"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRRL4Status" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRRL4Status" runat="server" ErrorMessage="Labor Status required."
                            ControlToValidate="cboSRRL4Status" SetFocusOnError="True" ValidationGroup="EmployeeRL4" Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblSRMedisType" runat="server" Text="Medis Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRMedisType" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <%--<asp:RequiredFieldValidator ID="rfvSRMedisType" runat="server" ErrorMessage="Medis Type required."
                    	        ControlToValidate="cboSRMedisType" SetFocusOnError="True" ValidationGroup="EmployeeRL4" Width="100%">
						        <asp:Image ID="Image5" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>--%>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRL4Type" runat="server" Text="RL4 Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRRL4Type" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboSRRL4Type_SelectedIndexChanged" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRRL4Type" runat="server" ErrorMessage="RL4 Type required."
                            ControlToValidate="cboSRRL4Type" SetFocusOnError="True" ValidationGroup="EmployeeRL4" Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeRL4"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="EmployeeRL4"
                            Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblSREducationLevel" runat="server" Text="Education Level"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREducationLevel" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <%--<asp:RequiredFieldValidator ID="rfvSREducationLevel" runat="server" ErrorMessage="Education Level Name required."
                            ControlToValidate="cboSREducationLevel" SetFocusOnError="True" ValidationGroup="EmployeeRL4" Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>--%>
                    </td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblRL4EducationID" runat="server" Text="RL4 Education Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboRL4EducationID" runat="server" Width="300px"
                            EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                            AutoPostBack="false" OnItemDataBound="cboRL4EducationID_ItemDataBound"
                            OnItemsRequested="cboRL4EducationID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "RL4EducationCode")%>
                                &nbsp;-&nbsp;
                                <%# DataBinder.Eval(Container.DataItem, "RL4EducationName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <%--<asp:RequiredFieldValidator ID="rfvRL4EducationID" runat="server" ErrorMessage="RL4 Education Name required."
            	            ControlToValidate="cboRL4EducationID" SetFocusOnError="True" ValidationGroup="EmployeePosition" Width="100%">
				            <asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
			            </asp:RequiredFieldValidator>--%>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRL4ProfessionType" runat="server" Text="Profession Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRRL4ProfessionType" runat="server" Width="300px"
                            EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                            AutoPostBack="true" OnItemDataBound="cboSRRL4ProfessionType_ItemDataBound"
                            OnItemsRequested="cboSRRL4ProfessionType_ItemsRequested" OnSelectedIndexChanged="cboSRRL4ProfessionType_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRRL4ProfessionType" runat="server" ErrorMessage="Profession Type required."
                            ControlToValidate="cboSRRL4ProfessionType" SetFocusOnError="True" ValidationGroup="EmployeeRL4" Width="100%">
                            <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRL4EducationLevel" runat="server" Text="Education Level"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRRL4EducationLevel" runat="server" Width="300px"
                            EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                            AutoPostBack="true" OnItemDataBound="cboSRRL4EducationLevel_ItemDataBound"
                            OnItemsRequested="cboSRRL4EducationLevel_ItemsRequested" OnSelectedIndexChanged="cboSRRL4EducationLevel_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRRL4EducationLevel" runat="server" ErrorMessage="Education Level required."
                            ControlToValidate="cboSRRL4EducationLevel" SetFocusOnError="True" ValidationGroup="EmployeeRL4" Width="100%">
                            <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRRL4EducationMajor" runat="server" Text="Major"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRRL4EducationMajor" runat="server" Width="300px"
                            EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                            AutoPostBack="false" OnItemDataBound="cboSRRL4EducationMajor_ItemDataBound"
                            OnItemsRequested="cboSRRL4EducationMajor_ItemsRequested">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRRL4EducationMajor" runat="server" ErrorMessage="Major required."
                            ControlToValidate="cboSRRL4EducationMajor" SetFocusOnError="True" ValidationGroup="EmployeeRL4" Width="100%">
                            <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>


                <tr style="display: none">
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                            ControlToValidate="txtValidFrom" SetFocusOnError="True" ValidationGroup="EmployeeRL4" Width="100%">
                            <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblValidTo" runat="server" Text="Valid To"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                            ControlToValidate="txtValidTo" SetFocusOnError="True" ValidationGroup="EmployeeRL4" Width="100%">
                            <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
