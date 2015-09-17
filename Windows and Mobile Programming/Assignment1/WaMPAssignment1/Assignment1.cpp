#include <windows.h>

#define LISTBOXONE 100
#define LISTBOXTWO 101
#define BUTTON 102

HWND hListOne, hListTwo, hButton;
int pos = 0;

LRESULT CALLBACK WinProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);
LRESULT CALLBACK ListOneProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);

int WINAPI WinMain(HINSTANCE hInst, HINSTANCE hPrevInst, LPSTR lpCmdLine, int nShowCmd)
{
	WNDCLASSEX wClass;
	ZeroMemory(&wClass, sizeof(WNDCLASSEX));
	wClass.cbClsExtra = NULL;
	wClass.cbSize = sizeof(WNDCLASSEX);
	wClass.cbWndExtra = NULL;
	wClass.hbrBackground = (HBRUSH)COLOR_WINDOW;
	wClass.hCursor = LoadCursor(NULL, IDC_ARROW);
	wClass.hIcon = NULL;
	wClass.hIconSm = NULL;
	wClass.hInstance = hInst;
	wClass.lpfnWndProc = (WNDPROC)WinProc;
	wClass.lpszClassName = "Window Class";
	wClass.lpszMenuName = NULL;
	wClass.style = CS_HREDRAW | CS_VREDRAW;

	if (!RegisterClassEx(&wClass))
	{
		int nResult = GetLastError();
		MessageBox(NULL,
			"Window class creation failed\r\n",
			"Window Class Failed",
			MB_ICONERROR);
	}

	HWND hWnd = CreateWindowEx(NULL,
		"Window Class",
		"Steven Johnston: Assignment 01",
		WS_OVERLAPPEDWINDOW,
		200,
		200,
		640,
		480,
		NULL,
		NULL,
		hInst,
		NULL);

	if (!hWnd)
	{
		int nResult = GetLastError();

		MessageBox(NULL,
			"Window creation failed\r\n",
			"Window Creation Failed",
			MB_ICONERROR);
	}

	ShowWindow(hWnd, nShowCmd);

	MSG msg;
	ZeroMemory(&msg, sizeof(MSG));

	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return 0;
}

LRESULT CALLBACK WinProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_CREATE:
	{
		// Create a Listbox
		// Notice that the structure of the listbox creation is very similar to the EDIT example
		hListOne = CreateWindowEx(WS_EX_CLIENTEDGE,
			"LISTBOX",
			"",
			WS_CHILD | WS_VISIBLE | LBS_NOTIFY,
			50,
			50,
			125,
			200,
			hWnd,
			(HMENU)LISTBOXONE,
			GetModuleHandle(NULL),
			NULL);
		HGDIOBJ hfDefault = GetStockObject(DEFAULT_GUI_FONT);
		SendMessage(hListOne,
			WM_SETFONT,
			(WPARAM)hfDefault,
			MAKELPARAM(FALSE, 0));

		SendMessage(hListOne,
			LB_ADDSTRING,
			NULL,
			(LPARAM)"John Smith");
		SendMessage(hListOne,
			LB_ADDSTRING,
			NULL,
			(LPARAM)"Mark Ryan");
		SendMessage(hListOne,
			LB_ADDSTRING,
			NULL,
			(LPARAM)"Jerry Hayes");
		SendMessage(hListOne,
			LB_ADDSTRING,
			NULL,
			(LPARAM)"Anthony Hodgins");
		SendMessage(hListOne,
			LB_ADDSTRING,
			NULL,
			(LPARAM)"Bart Simpson");


		hListTwo = CreateWindowEx(WS_EX_CLIENTEDGE,
			"LISTBOX",
			"",
			WS_CHILD | WS_VISIBLE,
			350,
			50,
			125,
			200,
			hWnd,
			(HMENU)LISTBOXTWO,
			GetModuleHandle(NULL),
			NULL);
		hfDefault = GetStockObject(DEFAULT_GUI_FONT);
		SendMessage(hListTwo,
			WM_SETFONT,
			(WPARAM)hfDefault,
			MAKELPARAM(FALSE, 0));

		//
		// Instead of setting text, we need to either add a string or insert a string
		// into a Listbox. In this case, adding a string is demonstrated.
		//
		

		// Create a push button
		hButton = CreateWindowEx(NULL,
			"BUTTON",
			"Move",
			WS_TABSTOP | WS_VISIBLE | WS_DISABLED|
			WS_CHILD | BS_DEFPUSHBUTTON,
			210,
			125,
			100,
			24,
			hWnd,
			(HMENU)BUTTON,
			GetModuleHandle(NULL),
			NULL);
		SendMessage(hButton,
			WM_SETFONT,
			(WPARAM)hfDefault,
			MAKELPARAM(FALSE, 0));
	}
	break;
	case WM_INITDIALOG:
	{

	case LISTBOXONE:
		// It's our listbox, check the notification code
		switch (HIWORD(wParam))
		{
		case LBN_SELCHANGE:
			// Selection changed, do stuff here.
			OutputDebugString("sdfosdfjjfjfjfj");
			break;
		}

		break;




	}
	break;
	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case BUTTON:
		{
			// Since a Listbox has several lines of text, you must first determine if
			// a line of text was selected. This is done with the LB_GETCURSEL command.
			// If nothing is selected, then the return value is LB_ERR, otherwise, the 
			// index (zero based) is returned.
			char buffer[256];
			int indexInList = (int)SendMessage(hListOne,
				LB_GETCURSEL,
				(WPARAM)0,
				(LPARAM)0);

			// If there was something selected, then we get it (using LB_GETTEXT) and
			// display it with the the MessageBox method.
			if (indexInList != LB_ERR)
			{
				SendMessage(hListOne,
					LB_GETTEXT,
					indexInList,
					reinterpret_cast<LPARAM>(buffer));
				SendMessage(hListOne,
					LB_DELETESTRING,
					indexInList,
					NULL);
				SendMessage(hListTwo,
					LB_ADDSTRING,
					NULL,
					(LPARAM) buffer);

				EnableWindow(GetDlgItem(hWnd, BUTTON), FALSE);
			}

			
		}
		break;

		case LISTBOXONE:
			switch (HIWORD(wParam))
			{
				case LBN_SELCHANGE:
					int indexInList = (int)SendMessage(hListOne,
						LB_GETCURSEL,
						(WPARAM)0,
						(LPARAM)0);
					//Item is selected

					if (indexInList != LB_ERR)
						EnableWindow(GetDlgItem(hWnd, BUTTON), TRUE);
					break;
			}
			
			break;
		}
		break;

	case WM_DESTROY:
	{
		PostQuitMessage(0);
		return 0;
	}
	break;
	}

	return DefWindowProc(hWnd, msg, wParam, lParam);
}

