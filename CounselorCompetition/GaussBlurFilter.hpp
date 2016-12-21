#pragma once

#include <stdlib.h>

#ifndef _USE_MATH_DEFINES
#define _USE_MATH_DEFINES
#endif
#include <math.h>

//
// ���½��ȵĻص�����ָ��
//

typedef bool (__stdcall *PF_UPD_PROGRESS)(LPVOID pProgressInfo, int nTotal, int nComplete);

//
// �����û�ȡ���Ļص�����ָ��
//

typedef bool (__stdcall *PF_TESTABORT)(void);

//
// �˾��ӿڣ����ڶ�̬��
//
class IFilter
{
protected:
	bool m_bMultiThreads;		//�Ƿ����ö��߳�
	int m_nThreadCount;			//�û�����Ķ��߳���
	LPVOID m_pProgInfo;			//���ȷ����Ĳ��������ظ��û�
	PF_UPD_PROGRESS m_pUpdProg; //���ȷ����ص�
	PF_TESTABORT m_pTestAbort; //����ȡ���ص�

public:
	IFilter()
	{
		m_bMultiThreads = false;
		m_nThreadCount = 1;

		m_pProgInfo = NULL;
		m_pUpdProg = NULL;
		m_pTestAbort = NULL;
	}
	virtual ~IFilter() { };
	virtual void Init(LPVOID pInfo) { }; //�������ò���
	virtual void Reset() { };

	bool GetMultiThreadEnabled() const {	return m_bMultiThreads; };
	int GetThreadCount() const { return m_nThreadCount; }
	PF_UPD_PROGRESS GetUpdateProgressFunc() const { return m_pUpdProg; };
	PF_TESTABORT GetTestAbortFunc() const { return m_pTestAbort; };

	void SetMultiThreads(bool bEnabled, int ThreadCount)
	{
		m_bMultiThreads = bEnabled;
		m_nThreadCount = ThreadCount;
	}

	void SetUpdateProgressFunc(PF_UPD_PROGRESS pF, LPVOID pInfo)
	{
		m_pUpdProg = pF;
		m_pProgInfo = pInfo;
	}

	void SetTestAbortFunc(PF_TESTABORT pF) { m_pTestAbort = pF; };

	virtual bool UpdateProgress(int nTotal, int nComplete) const
	{
		if(m_pUpdProg == NULL)
			return false;

		return m_pUpdProg(m_pProgInfo, nTotal, nComplete);
	}

	virtual bool TestAbort() const
	{
		if(m_pTestAbort == NULL)
			return false;

		return m_pTestAbort();
	}

	virtual bool Filter(LPCVOID pSrc, LPVOID pDest, int width, int height, int bpp)
	{
		return false;
	}
};

//
// Thread Function Declares;
//

template<typename T>
DWORD WINAPI GaussBlurThreadProc8(LPVOID lpParameters);

template<typename T>
DWORD WINAPI GaussBlurThreadProc24(LPVOID lpParameters);

//
// ���ݵ��̵߳Ĳ���
//
template<typename T>
class CGaussBlurThreadParams
{
public:
	int r;
	T* pTempl;
	LPBYTE pSrc;	//Src  λͼ��λͼ������� ���������̣߳�pSrc��pDest ����ͬ�ģ�
	LPBYTE pDest;	//Dest λͼ��λͼ�������

	int width;		//ͼ����
	int height;		//ͼ��߶ȣ��Ѿ���ȡ����ֵ��

	//������з�Χ��[rowBegin, rowEnd) �������� rowEnd ��ÿ���̲߳�ͬ��
	int rowBegin;
	int rowEnd;
	int stride;		//ɨ���п�ȣ�bytes��
	int pixelSize;	//���ش�С =bpp/8;

	bool bHorz;		//true-ˮƽģ����false-����ģ��

public:
	CGaussBlurThreadParams() { };
	~CGaussBlurThreadParams() { };
};

//
// ��˹ģ���㷨
//

template<typename T>
class CGaussBlurFilter : public IFilter
{
protected:
	int m_r;		//����ģ�������εı߳�Ϊ (2 * r + 1)
	T m_sigma;		//��˹�뾶�������ƽ������
	T* m_pTempl;	//ģ��T[r+1];

public:
	CGaussBlurFilter();
	virtual ~CGaussBlurFilter();

	int GetR() const { return m_r; };
	T GetSigma() const { return m_sigma; };

	//���ò�������ʱ�����ģ�壩
	void SetSigma(T sigma);

	//���ò���
	virtual void Init(LPVOID pInfo);

	//�ͷ�ģ��
	virtual void Reset();

	//����ͼ��
	virtual bool Filter(LPCVOID pSrc, LPVOID pDest, int width, int height, int bpp);
};

template<typename T>
CGaussBlurFilter<T>::CGaussBlurFilter()
{
	m_r = -1;
	m_sigma = (T)(-1);
	m_pTempl = NULL;
	m_bMultiThreads = false;
	m_nThreadCount = 0;
}

template<typename T>
CGaussBlurFilter<T>::~CGaussBlurFilter()
{
	if(m_pTempl != NULL)
		free(m_pTempl);
}

template<typename T>
void CGaussBlurFilter<T>::SetSigma(T sigma)
{
	int i;
	m_sigma = sigma;
	m_r = (int)(m_sigma * 3 + 0.5);
	if(m_r <= 0) m_r = 1;

	//����ģ��
	LPVOID pOldTempl = m_pTempl;
	m_pTempl = (T*)realloc(m_pTempl, sizeof(T) * (m_r + 1));

	//����ʧ�ܣ�
	if(m_pTempl == NULL)
	{
		if(pOldTempl != NULL)
			free(pOldTempl);

		return;
	}
	
	//���� p[0] �Ҷ�ֵΪ1 ��ģ����
	T k1 = (T)((-0.5) / (m_sigma * m_sigma));
	for(i = 0; i <= m_r; i++)
		m_pTempl[i] = exp(k1 * i * i);

	//����ģ���Ȩ�ܺ�
	T sum = m_pTempl[0];
	for(i = 1; i <= m_r; i++)
	{
		sum += (m_pTempl[i] * 2);
	}
	
	//��һ��
	sum = (T)(1.0 / sum); //ȡ����
	for(i = 0; i <= m_r; i++)
		m_pTempl[i] *= sum;
}

//
// ��CGaussBlurFilter �У�pInfo ��һ��ָ�� T sigma ��ָ�룻
//
template<typename T>
void CGaussBlurFilter<T>::Init(LPVOID pInfo)
{
	T* pT = (T*)pInfo;
	SetSigma(*pT);
}

template<typename T>
void CGaussBlurFilter<T>::Reset()
{
	m_r = -1;
	m_sigma = (T)(-1.0);
	if(m_pTempl != NULL)
	{
		free(m_pTempl);
		m_pTempl = NULL;
	}
}

template<typename T>
bool CGaussBlurFilter<T>::Filter(LPCVOID pSrc, LPVOID pDest, int width, int height, int bpp)
{
	if(pSrc == NULL || pDest == NULL)
		return false;

	//ֻ�ܴ��� 8, 24�� 32 bpp
	if(bpp != 24 && bpp != 8 && bpp != 32)
		return false;

	if(m_r < 0 || m_pTempl == NULL)
		return false;

	int absHeight = (height >= 0) ? height : (-height);
	int stride = (width * bpp + 31)/ 32 * 4;
	int pixelSize = bpp / 8;
	int i, ThreadCount;
	DWORD dwTid;

	//���뻺�������洢�м���
	LPVOID pTemp = malloc(stride * absHeight);
	if(pTemp == NULL)
		return false;

	//�ж��Ƿ����ö��̴߳���
	if(m_bMultiThreads && m_nThreadCount > 1)
	{
		ThreadCount = min(m_nThreadCount, absHeight);

		CGaussBlurThreadParams<T> *p1 = new CGaussBlurThreadParams<T>[ThreadCount];
		HANDLE *pHandles = new HANDLE[ThreadCount];

		//ˮƽ����
		for(i = 0; i < ThreadCount; i++)
		{
			p1[i].pSrc = (LPBYTE)pSrc;
			p1[i].pDest = (LPBYTE)pTemp;
			p1[i].width = width;
			p1[i].height = absHeight;
			p1[i].stride = stride;
			p1[i].pixelSize = pixelSize;
			p1[i].r = m_r;
			p1[i].pTempl = m_pTempl;

			//����������
			p1[i].rowBegin = absHeight / ThreadCount * i;

			//�߶Ȳ�һ���ܱ� ThreadCount �������������һ������Ľ�β������ʽָ����
			if(i == ThreadCount - 1)
				p1[i].rowEnd = absHeight;
			else
				p1[i].rowEnd = p1[i].rowBegin + absHeight / ThreadCount;

			p1[i].bHorz = true;

			//Committed StackSize = 512; �߳���Ҫ��ջ�Ĵ�С
			pHandles[i] = CreateThread(NULL, 512, 
				(bpp == 8)? GaussBlurThreadProc8<T> : GaussBlurThreadProc24<T>,
				(LPVOID)(&p1[i]), 0, &dwTid);
		}
		WaitForMultipleObjects(ThreadCount, pHandles, TRUE, INFINITE);
		for(i = 0; i < ThreadCount; i++)
			CloseHandle(pHandles[i]);

		UpdateProgress(100, 50);

		//��ֱ����
		for(i = 0; i < ThreadCount; i++)
		{
			p1[i].pSrc = (LPBYTE)pTemp;
			p1[i].pDest = (LPBYTE)pDest;
			p1[i].bHorz = false;

			pHandles[i] = CreateThread(NULL, 512, 
				(bpp == 8)? GaussBlurThreadProc8<T> : GaussBlurThreadProc24<T>,
				(LPVOID)(&p1[i]), 0, &dwTid);
		}
		WaitForMultipleObjects(ThreadCount, pHandles, TRUE, INFINITE);
		for(i = 0; i < ThreadCount; i++)
			CloseHandle(pHandles[i]);

		delete[] p1;
		delete[] pHandles;
	}
	else
	{
		//���߳�
		CGaussBlurThreadParams<T> params;

		params.pSrc = (LPBYTE)pSrc;
		params.pDest = (LPBYTE)pTemp;
		params.width = width;
		params.height = absHeight;
		params.stride = stride;
		params.pixelSize = pixelSize;
		params.r = m_r;
		params.pTempl = m_pTempl;
		params.rowBegin = 0;
		params.rowEnd = absHeight;
		params.bHorz = true;

		if(bpp == 8)
			GaussBlurThreadProc8<T>(&params);
		else
			GaussBlurThreadProc24<T>(&params);

		UpdateProgress(100, 50);

		params.pSrc = (LPBYTE)pTemp;
		params.pDest = (LPBYTE)pDest;
		params.bHorz = false;
		
		if(bpp == 8)
			GaussBlurThreadProc8<T>(&params);
		else
			GaussBlurThreadProc24<T>(&params);
	}

	free(pTemp);
	UpdateProgress(100, 100);
	return true;
}

//�̵߳���ڵ�: ���� 8 bpp ͼ�񣨻Ҷ�ͼ��ȫ������
template<typename T>
DWORD WINAPI GaussBlurThreadProc8(LPVOID lpParameters)
{
	CGaussBlurThreadParams<T> *pInfo = (CGaussBlurThreadParams<T>*)lpParameters;

	T result;
	int row, col, subRow, subCol, MaxVal, x, x1;
	LPINT pSubVal, pRefVal;

	if(pInfo->bHorz)
	{
		//ˮƽ����
		pSubVal = &subCol;
		pRefVal = &col;
		MaxVal = pInfo->width - 1;
	}
	else
	{
		//��ֱ����
		pSubVal = &subRow;
		pRefVal = &row;
		MaxVal = pInfo->height - 1;
	}
	
	LPBYTE pSrcPixel = NULL;
	LPBYTE pDestPixel = NULL;

	for(row = pInfo->rowBegin; row < pInfo->rowEnd; ++row)
	{
		for(col = 0; col < pInfo->width; ++col)
		{
			pDestPixel = pInfo->pDest + pInfo->stride * row + col;

			result = 0;

			subRow = row;
			subCol = col;

			for(x = -pInfo->r; x <= pInfo->r; x++)
			{
				//�߽紦��
				x1 = (x >= 0) ? x : (-x);
				*pSubVal = *pRefVal + x;
				if(*pSubVal < 0) *pSubVal = 0;
				else if(*pSubVal > MaxVal) *pSubVal = MaxVal;
				
				pSrcPixel = pInfo->pSrc + pInfo->stride * subRow + subCol;

				result += *pSrcPixel * pInfo->pTempl[x1];
			}
			*pDestPixel = (BYTE)result;
		}
	}
	return 0;
}

//�̵߳���ڵ�: ���� 24 bpp ����ͼ��
template<typename T>
DWORD WINAPI GaussBlurThreadProc24(LPVOID lpParameters)
{
	CGaussBlurThreadParams<T> *pInfo = (CGaussBlurThreadParams<T>*)lpParameters;

	T result[3];
	int row, col, subRow, subCol, MaxVal, x, x1;
	LPINT pSubVal, pRefVal;

	if(pInfo->bHorz)
	{
		//ˮƽ����
		pSubVal = &subCol;
		pRefVal = &col;
		MaxVal = pInfo->width - 1;
	}
	else
	{
		//��ֱ����
		pSubVal = &subRow;
		pRefVal = &row;
		MaxVal = pInfo->height - 1;
	}
	
	LPBYTE pSrcPixel = NULL;
	LPBYTE pDestPixel = NULL;

	for(row = pInfo->rowBegin; row < pInfo->rowEnd; ++row)
	{
		for(col = 0; col < pInfo->width; ++col)
		{
			pDestPixel = pInfo->pDest + pInfo->stride * row + pInfo->pixelSize * col;

			result[0] = 0;
			result[1] = 0;
			result[2] = 0;

			subRow = row;
			subCol = col;

			for(x = -pInfo->r; x <= pInfo->r; x++)
			{
				x1 = (x >= 0) ? x : (-x);
				*pSubVal = *pRefVal + x;

				//�߽紦��Photoshop ���õ��Ƿ���1��
				//����1��ȡ��Ե���أ�ͼ���Ե�������ڲ���ɢ����
				if(*pSubVal < 0) *pSubVal = 0;
				else if(*pSubVal > MaxVal) *pSubVal = MaxVal;

				//����2��ȡ��ǰ���أ�ʹ��Խ����ͼ���Ե�ĵط�Խ������
				/*
				if(*pSubVal < 0 || *pSubVal > MaxVal)
					*pSubVal = *pRefVal;
				*/
				
				pSrcPixel = pInfo->pSrc + pInfo->stride * subRow + pInfo->pixelSize * subCol;

				result[0] += pSrcPixel[0] * pInfo->pTempl[x1];
				result[1] += pSrcPixel[1] * pInfo->pTempl[x1];
				result[2] += pSrcPixel[2] * pInfo->pTempl[x1];
			}
			pDestPixel[0] = (BYTE)result[0];
			pDestPixel[1] = (BYTE)result[1];
			pDestPixel[2] = (BYTE)result[2];
		}
	}
	return 0;
}