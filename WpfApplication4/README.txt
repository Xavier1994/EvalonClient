项目说明：

本项目是一个WPF项目，但项目主要使用ADO .NET作为技术支持，在ADO .NET中，与数据库交互有三种方式，在本项目中都有使用，
其中连接层和断开连接层都待扩展，而EF则已经构建完毕，可以直接使用，后续开发者可以选择自己喜欢的数据交互方式使用，
并在适当的地方扩展.

推荐使用EF,并且使用LINQ TO EF作为查询和修改的工具.
It is powerful and awesome！！！


 <Label Content="我的课程表" HorizontalAlignment="Left" Margin="286,10,0,0" VerticalAlignment="Top" FontSize="18px" FontWeight="Bold" FontStyle="Normal"/>
        <Grid Margin="0,58,0,2" Background="#FF0C7272">
            <Grid ShowGridLines="False">
                <Grid.RowDefinitions>
                    <RowDefinition Height="25" />
                    <RowDefinition Height="25" />
                    <RowDefinition Height="25" />
                    <RowDefinition Height="25" />
                    <RowDefinition Height="25" />
                    <RowDefinition Height="25" />
                    <RowDefinition Height="25" />
                    <RowDefinition Height="25" />
                    <RowDefinition Height="25" />
                    <RowDefinition Height="25" />
                    <RowDefinition Height="25" />
                    <RowDefinition Height="25" />
                    <RowDefinition Height="25" />
                </Grid.RowDefinitions>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition/>
                    <ColumnDefinition/>
                    <ColumnDefinition/>
                    <ColumnDefinition/>
                    <ColumnDefinition/>
                    <ColumnDefinition/>
                    <ColumnDefinition/>
                </Grid.ColumnDefinitions>
                <Border BorderBrush="Black" BorderThickness="1" Grid.Column="0" Grid.Row="0"/>
                <Border BorderBrush="Black" BorderThickness="1" Grid.Column="0" Grid.Row="1"/>
                <Border BorderBrush="Black" BorderThickness="1" Grid.Column="0" Grid.Row="2"/>
                <Border BorderBrush="Black" BorderThickness="1" Grid.Column="0" Grid.Row="3"/>
                <Border BorderBrush="Black" BorderThickness="1" Grid.Column="0" Grid.Row="4"/>
                <Border BorderBrush="Black" BorderThickness="1" Grid.Column="0" Grid.Row="5"/>
                <Border BorderBrush="Black" BorderThickness="1" Grid.Column="0" Grid.Row="6"/>
                <Border BorderBrush="Black" BorderThickness="1" Grid.Column="0" Grid.Row="7"/>
                <Border BorderBrush="Black" BorderThickness="1" Grid.Column="0" Grid.Row="8"/>
                <Border BorderBrush="Black" BorderThickness="1" Grid.Column="0" Grid.Row="9"/>
                <Border BorderBrush="Black" BorderThickness="1" Grid.Column="0" Grid.Row="10"/>
                <Border BorderBrush="Black" BorderThickness="1" Grid.Column="0" Grid.Row="11"/>
                <Border BorderBrush="Black" BorderThickness="1" Grid.Column="0" Grid.Row="12"/>

                <TextBlock Grid.Row="0" Grid.Column="0" Text="星期一" FontSize="18" Grid.ColumnSpan="2" Margin="20,0,75,0"/>
                <TextBlock Grid.Row="0" Grid.Column="1" Text="星期二" FontSize="18" Grid.ColumnSpan="2" Margin="20,0,75,0"/>
                <TextBlock Grid.Row="0" Grid.Column="2" Text="星期三" FontSize="18" Grid.ColumnSpan="2" Margin="20,0,75,0"/>
                <TextBlock Grid.Row="0" Grid.Column="3" Text="星期四" FontSize="18" Grid.ColumnSpan="2" Margin="20,0,75,0"/>
                <TextBlock Grid.Row="0" Grid.Column="4" Text="星期五" FontSize="18" Grid.ColumnSpan="2" Margin="20,0,75,0"/>
                <TextBlock Grid.Row="0" Grid.Column="5" Text="星期六" FontSize="18" Grid.ColumnSpan="2" Margin="20,0,75,0"/>
                <TextBlock Grid.Row="0" Grid.Column="6" Text="星期日" FontSize="18" Margin="20,0,10,0"/>