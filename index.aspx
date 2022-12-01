<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Spotify_App.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <create>
                <h1>Spotify</h1>
                <hr />
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <hr />
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0">Search songs By Artist Name</asp:ListItem>
                <asp:ListItem Value="1">Search Songs By Album Name</asp:ListItem>
                <asp:ListItem Value="2">Search Songs By Song Name</asp:ListItem>
                <asp:ListItem Value="3">Show all Artists</asp:ListItem>
                <asp:ListItem Value="4">Show all Albums</asp:ListItem>
                <asp:ListItem Value="5">Show all Songs</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Label ID="lblsearch" runat="server" Text="Enter Artist Name: "></asp:Label><asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="Button1" runat="server" Text="Submit" Onclick="Button1_Click"/>
                <hr />
                <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            </create>
        </div>
    </form>
</body>
</html>
