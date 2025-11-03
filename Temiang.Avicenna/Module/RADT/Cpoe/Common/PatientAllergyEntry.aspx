<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="PatientAllergyEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.PatientAllergyEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:HiddenField ID="hfSatuSehatOrganizationID" runat="server" />
    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            var satuSehatOrganizationID = document.getElementById('<%= hfSatuSehatOrganizationID.ClientID %>').value;
            var isbridgingss = satuSehatOrganizationID && satuSehatOrganizationID.trim() !== '';

            if (isbridgingss) {
                document.getElementById('fieldsetPatientAllergy').style.display = 'block';
            } else {
                document.getElementById('fieldsetPatientAllergy').style.display = 'none';
            }
        });
        function cboDrugAllergy_ClientItemsRequesting(sender, eventArgs) {
            var context = eventArgs.get_context();
            context["tp"] = "kfaza";
        }
        function onSatuSehatItemClick(name) {
            var txt = $find("ctl00_ContentPlaceHolder1_grdPatientAllergySS_ctl00_ctl04_txtAllergenDesc");
            txt.set_value(name);
        }
    </script>
    <fieldset id="fieldsetPatientAllergy">
        <legend>
            <asp:Label ID="label1" runat="server" Text="Patient Allergy Drugs (SatuSehat)" Font-Bold="True" Font-Size="9"></asp:Label>
        </legend>
        <telerik:RadGrid ID="grdPatientAllergySS" runat="server" AutoGenerateColumns="False" EnableViewState="true" GridLines="None" OnNeedDataSource="grdPatientAllergySS_NeedDataSource" OnItemDataBound="grdPatientAllergySS_ItemDataBound">
            <MasterTableView DataKeyNames="ItemID" GroupLoadMode="Client">
                <Columns>              
                <telerik:GridBoundColumn DataField="StandardReferenceID" Display="False" UniqueName="StandardReferenceID" />
                    <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID" Display="False" />
                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Category" UniqueName="ItemName" Display="False" />
                    <telerik:GridTemplateColumn HeaderText="Code" UniqueName="TemplateColumn1">
                        <ItemTemplate>
                            <telerik:RadComboBox ID="cboDrugAllergy" runat="server" Width="450px" EmptyMessage="Select" EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" OnClientItemsRequesting="cboDrugAllergy_ClientItemsRequesting">
                                <WebServiceSettings Method="SatuSehatItem" Path="~/WebService/ComboBoxDataService.asmx" />
                                <ClientItemTemplate>
                                <div onclick="onSatuSehatItemClick('#= Attributes.ItemName #')">
                                    <ul class="details">
                                        <li class="small"><span>#= Attributes.ItemName #</span></li>
                                    </ul>
                                </div>
                                </ClientItemTemplate>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="rfvDrugAllergy" runat="server" ControlToValidate="cboDrugAllergy" InitialValue="" ErrorMessage="Drug Allergy is required" Display="Dynamic"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Zat Active" UniqueName="TemplateColumn1">
                        <ItemTemplate>
                            <telerik:RadTextBox ID="txtAllergenDesc" runat="server" Width="500px" MaxLength="4000" Text='<%# DataBinder.Eval(Container.DataItem, "DescAndReaction") %>' />
                            <asp:RequiredFieldValidator ID="rfvAllergenDesc" runat="server" ControlToValidate="txtAllergenDesc" InitialValue="" ErrorMessage="Description is required" Display="Dynamic"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Verif Status" UniqueName="TemplateColumnVerifStatus">
                        <ItemTemplate>
                            <telerik:RadComboBox ID="cboAllergyVerif" runat="server" Width="100px" EmptyMessage="Select" />
                            <asp:RequiredFieldValidator ID="rfvAllergyVerif" runat="server" ControlToValidate="cboAllergyVerif" InitialValue="" ErrorMessage="Verification Status is required" Display="Dynamic"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Status" UniqueName="TemplateColumnStatus">
                        <ItemTemplate>
                            <telerik:RadComboBox ID="cboAllergyStatus" runat="server" Width="100px" EmptyMessage="Select" />
                            <asp:RequiredFieldValidator ID="rfvAllergyStatus" runat="server" ControlToValidate="cboAllergyStatus" InitialValue="" ErrorMessage="Status is required" Display="Dynamic"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="From Date">
                        <ItemTemplate>
                            <telerik:RadDatePicker ID="txtAllergenDate" runat="server" Width="100px" SelectedDate='<%# Eval("AllergenDate") != DBNull.Value ? (DateTime?)Eval("AllergenDate") : null %>' />
                            <asp:RequiredFieldValidator ID="rfvAllergenDate" runat="server" ControlToValidate="txtAllergenDate" InitialValue="" ErrorMessage="From Date is required" Display="Dynamic"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <FilterMenu>
            </FilterMenu>
            <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                <Resizing AllowColumnResize="False" />
            </ClientSettings>
        </telerik:RadGrid>
    </fieldset>
    <telerik:RadGrid ID="grdPatientAllergy" runat="server" AutoGenerateColumns="False" EnableViewState="true"
        GridLines="None" OnNeedDataSource="grdPatientAllergy_NeedDataSource">
        <MasterTableView DataKeyNames="ItemID" GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="Group" HeaderText="." />
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="Group" SortOrder="Ascending" />
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn DataField="StandardReferenceID" Display="False" UniqueName="StandardReferenceID" />
                <telerik:GridBoundColumn DataField="ItemID" Display="False" UniqueName="ItemID" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Allergen Name" SortExpression="ItemName"
                    UniqueName="ItemName" HeaderStyle-Width="20%" />
                <telerik:GridTemplateColumn HeaderText="Description &amp; Reaction" UniqueName="TemplateColumn1">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtAllergenDesc" runat="server" Width="100%" MaxLength="4000"
                            Text='<%# DataBinder.Eval(Container.DataItem, "DescAndReaction") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="From Date">
                    <ItemTemplate>
                        <telerik:RadDatePicker ID="txtAllergenDate" runat="server" Width="100px" SelectedDate='<%# Eval("AllergenDate") != DBNull.Value ? (DateTime?)Eval("AllergenDate") : null %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
            <Resizing AllowColumnResize="False" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
