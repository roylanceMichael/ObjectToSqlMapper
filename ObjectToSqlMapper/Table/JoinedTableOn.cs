﻿namespace ObjectToSqlMapper.Table
{
	using ObjectToSqlMapper.Utils;

	public class JoinedTableOn : ISqlConstituent
	{
		private const string JoinedTemplate = StringExtensions.Tab + " ON {0}.{1} = {2}.{3}";

		private readonly ISqlConstituent parentTableColumn;

		private readonly ISqlConstituent joinedTableColumn;

		private readonly IAliasable parentTableAlias;

		private readonly IAliasable joinedTableAlias;

		public JoinedTableOn(ISqlConstituent parentTableColumn, ISqlConstituent joinedTableColumn, IAliasable parentTableAlias, IAliasable joinedTableAlias)
		{
			parentTableColumn.CheckWhetherArgumentIsNull("parentTableColumn");
			joinedTableColumn.CheckWhetherArgumentIsNull("joinedTableColumn");
			parentTableAlias.CheckWhetherArgumentIsNull("parentTableAlias");
			joinedTableAlias.CheckWhetherArgumentIsNull("joinedTableAlias");

			this.parentTableColumn = parentTableColumn;
			this.joinedTableColumn = joinedTableColumn;
			this.parentTableAlias = parentTableAlias;
			this.joinedTableAlias = joinedTableAlias;
		}

		public string Name
		{
			get
			{
				return string.Empty;
			}
		}

		public string Expression
		{
			get
			{
				return JoinedTemplate.FormatCurrentCulture(
					this.parentTableAlias.Alias, 
					this.parentTableColumn.Name, 
					this.joinedTableAlias.Alias, 
					this.joinedTableColumn.Name);
			}
		}
	}
}
