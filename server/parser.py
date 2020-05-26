import requests
from lxml import etree
import lxml.html


def parse(url):
    try:
        api = requests.get(url)
    except:
        return
    tree = lxml.html.document_fromstring(api.text)

    for x in range(0, 5):
        # получаю url у пяти первых новостей
        news = tree.xpath(
            '//*[@id="postListContainer"]//*[@class="post-title"]/a')[x].get('href')
        # print(news)
        try:
            api_news = requests.get(news)
            tree_news = lxml.html.document_fromstring(api_news.text)
            # заголовок новосто
            head = tree_news.xpath('//*[@id="post-item"]/div[5]/h1/text()')
            # картинка за новостью (ссылка на нее) - чаще есть,
			# чем нет - можно парсить только такие новости,
            # чтоб легче было перенести
            # pic = tree_news.xpath('//*[@id="post-item"]/div[6]/figure[1]/a')[0].get('href')
            # тело новости, а вот тут и начинается грязь
            # внутри тега "p" много другие тегов
            # из-за этого не могу взять цельную новость
            # но если она без лишних тегов - то без проблем
            body = tree_news.xpath('//*[@id="post-item"]/div[6]/p/text()')
            print(head[0])
            # print(pic)
            for _x in body:
                print(_x)
        except:
            print('???')


def main():
    # взял сайта только анонсы игр
    parse('https://www.playground.ru/news/announces')
    # parse("https://www.playground.ru/detroit_become_human/v_steam_poyavilas_stranitsa_i_demoversiya_detroit_become_human-721875")


if __name__ == '__main__':
    main()
