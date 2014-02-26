<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ÄventyrligaKontakter.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Äventyrliga kontakter</title>
    <link href="~/Content/styling.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="theForm" runat="server">
        <h1>Äventyrliga kontakter</h1>
        <div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Fel inträffade. Korrigera det som är fel och försök igen."
                CssClass="validation-summary-errors" ValidationGroup="InsertValidation" />
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" HeaderText="Fel inträffade. Korrigera det som är fel och försök igen."
                CssClass="validation-summary-errors" ValidationGroup="EditValidation" />

            <asp:ListView ID="ContactListView" runat="server"
                ItemType="ÄventyrligaKontakter.Model.Contact"
                SelectMethod="ContactListView_GetData"
                InsertMethod="ContactListView_InsertItem"
                UpdateMethod="ContactListView_UpdateItem"
                DeleteMethod="ContactListView_DeleteItem"
                DataKeyNames="ContactId"
                InsertItemPosition="FirstItem">
                <LayoutTemplate>
                    <table>
                        <tr>
                            <th>Förnamn
                            </th>
                            <th>Efternamn
                            </th>
                            <th>Epostadress
                            </th>
                        </tr>
                        <%-- Platshållare för nya rader --%>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#: Item.FirstName %>
                        </td>
                        <td>
                            <%#: Item.LastName %>
                        </td>
                        <td>
                            <%#: Item.EmailAddress %>
                        </td>

                        <td>
                            <asp:LinkButton runat="server" CommandName="Delete" Text="Ta bort" OnClientClick='<%# String.Format("return confirm (\"Är du säker att du vill ta bort {0} {1}?\")", Item.FirstName, Item.LastName) %>' CausesValidation="false" />
                            <asp:LinkButton runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td>Kunduppgifter saknas.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <tr>
                        <td>
                            <asp:TextBox ID="InsertFirstName" runat="server" Text='<%# BindItem.FirstName %>' MaxLength="50" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ett förnamn måste anges" ControlToValidate="InsertFirstName" Display="None" ValidationGroup="InsertValidation"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="InsertLastName" runat="server" Text='<%# BindItem.LastName %>' MaxLength="50" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Ett efternamn måste anges" ControlToValidate="InsertLastName" Display="None" ValidationGroup="InsertValidation"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="InsertEmailAdress" runat="server" Text='<%# BindItem.EmailAddress %>' MaxLength="50" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="En e-postadress måste anges" ControlToValidate="InsertEmailAdress" Display="None" ValidationGroup="InsertValidation"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Ogiltig e-postadress" ControlToValidate="InsertEmailAdress" Display="None" ValidationExpression="^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$" ValidationGroup="InsertValidation"></asp:RegularExpressionValidator>

                        </td>
                        <td>
                            <asp:LinkButton runat="server" CommandName="Insert" Text="Lägg till" />
                            <asp:LinkButton runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <EditItemTemplate>
                    <tr>
                        <td>
                            <asp:TextBox ID="EditFirstName" runat="server" Text='<%# BindItem.FirstName %>' MaxLength="50" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Ett förnamn måste anges" ControlToValidate="EditFirstName" Display="None" ValidationGroup="EditValidation"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="EditLastName" runat="server" Text='<%# BindItem.LastName %>' MaxLength="50" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Ett efternamn måste anges" ControlToValidate="EditLastName" Display="None" ValidationGroup="EditValidation"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="EditEmailAdress" runat="server" Text='<%# BindItem.EmailAddress %>' MaxLength="50" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="En e-postadress måste anges" ControlToValidate="EditEmailAdress" Display="None" ValidationGroup="EditValidation"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Ogiltig e-postadress" ControlToValidate="EditEmailAdress" Display="None" ValidationExpression="^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$" ValidationGroup="EditValidation"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:LinkButton runat="server" CommandName="Update" Text="Spara" />
                            <asp:LinkButton runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                        </td>
                    </tr>
                </EditItemTemplate>
            </asp:ListView>


        </div>
    </form>
</body>
</html>
