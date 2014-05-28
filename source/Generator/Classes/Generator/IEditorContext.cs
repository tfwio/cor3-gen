using System;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;

namespace Generator
{
	public interface IEditorContext {
		
		CompletionWindow CompletionWindow { get; set; }
		FoldingManager FoldingManager { get; set; }
		AbstractFoldingStrategy FoldingStrategy { get; set; }
		AvalonEditor.Editor Editor { get; }
		DispatcherTimer FoldingUpdateTimer { get;set; }
	}
}
